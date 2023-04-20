using LocalNativeProvider.BusinessObjects;
using LocalNativeProvider.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LocalNativeProvider.Controllers;

[Route("api/local-native-provider/objects")]
public class ObjectsController : ControllerBase
{
    [HttpGet]
    public PaginatedResponse<BusinessObjectResource> Get()
    {
        var protocol = Request.Scheme;
        var host = Request.Host.ToString();

        var data = BusinessObjectList.Instance.Select(businessObject =>
            new BusinessObjectResource(businessObject, protocol, host));

        return new PaginatedResponse<BusinessObjectResource>
        {
            Data = data
        };
    }

    public record BusinessObjectResource()
    {
        public BusinessObjectResource(IBusinessObject businessObject, string protocol, string host) : this()
        {
            Name = businessObject.Name;
            DisplayName = businessObject.DisplayName;
            Description = businessObject.Description;

            SchemaUrl = $"{protocol}://{host}/api/local-native-provider/schema/{businessObject.Name}";
            DataUrl = $"{protocol}://{host}/api/local-native-provider/data/{businessObject.Name}";
        }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public IEnumerable<string> Areas { get; } = BusinessObjectAreas.Value;
        public string SchemaUrl { get; }
        public string DataUrl { get; }
    }
}
