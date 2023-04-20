using LocalNativeProvider.BusinessObjects;
using LocalNativeProvider.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace LocalNativeProvider.Controllers;

[Route("api/local-native-provider/data/{businessObjectName}")]
public class DataController : ControllerBase
{
    private readonly NpgsqlDataSource _dataSource;

    public DataController(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    [HttpPost]
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromRoute] string businessObjectName, [FromBody] DataRequest req, [FromQuery] int skip = 0, [FromQuery] int take = 500)
    {
        var businessObject = BusinessObjectList.Instance.FirstOrDefault(x => x.Name == businessObjectName);
        if (businessObject is null)
        {
            return NotFound();
        }

        IEnumerable<object> data;
        await using (var connection = await _dataSource.OpenConnectionAsync())
        {
            data = await businessObject.GetDataAsync(connection, req.Filter, skip, take + 1);
        }

        var dataAsList = data.ToList();
        var dataToReturn = (dataAsList.Count == take + 1) ? dataAsList.Take(take).ToList() : dataAsList;
        var nextPageUrl = (dataAsList.Count == take + 1) ? $"{Request.Scheme}://{Request.Host}/api/local-native-provider/data/{businessObjectName}?skip={skip + take}&take={take}" : null;

        return Ok(new PaginatedResponse<object>()
        {
            Data = dataToReturn,
            NextPageUrl = nextPageUrl
        });
    }

    public record DataRequest
    {
        public IEnumerable<string> Properties { get; set; }
        public BusinessObjectFilter Filter { get; set; }
    }
}
