using LocalNativeProvider.BusinessObjects;
using LocalNativeProvider.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LocalNativeProvider.Controllers;

[Route("api/local-native-provider/options/{businessObjectName}/{propertyName}")]
public class OptionsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromRoute] string businessObjectName, [FromRoute] string propertyName)
    {
        var businessObject = BusinessObjectList.Instance.FirstOrDefault(x => x.Name == businessObjectName);
        if (businessObject is null)
        {
            return NotFound();
        }

        var property = businessObject.Properties.FirstOrDefault(x => x.Name == propertyName);
        if (property is null)
        {
            return NotFound();
        }

        var data = property.Filter?.Options?.Select(x => new OptionResource(x.Key, x.Value)).ToList();
        if (data is null)
        {
            return NotFound();
        }

        var result = new PaginatedResponse<OptionResource>()
        {
            Data = data
        };

        return Ok(result);
    }

    public record OptionResource(object Key, string Label);
}