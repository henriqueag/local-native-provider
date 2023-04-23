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

        var data = BusinessObjectList.Instance.Select(businessObject => new BusinessObjectResource(businessObject, Request));

        return new PaginatedResponse<BusinessObjectResource>
        {
            Data = data
        };
    }

    public record BusinessObjectResource()
    {
        public BusinessObjectResource(IBusinessObject businessObject, HttpRequest request) : this()
        {
            Name = businessObject.Name;
            DisplayName = businessObject.DisplayName;
            Description = businessObject.Description;

            SchemaUrl = $"{request.Scheme}://{request.Host}/api/local-native-provider/schema/{businessObject.Name}";
            DataUrl = $"{request.Scheme}://{request.Host}/api/local-native-provider/data/{businessObject.Name}";

            Properties = businessObject.Properties.Select(property => new BusinessObjectSchemaPropertyResource(businessObject, property, request));
        }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public IEnumerable<string> Areas { get; } = BusinessObjectAreas.Value;
        public string SchemaUrl { get; }
        public string DataUrl { get; }
        public IEnumerable<BusinessObjectSchemaPropertyResource> Properties { get; }
    }

    public record BusinessObjectSchemaPropertyResource
    {
        public BusinessObjectSchemaPropertyResource(IBusinessObject businessObject, BusinessObjectProperty property, HttpRequest request)
        {
            Type = property.Type;
            Name = property.Name;
            DisplayName = property.DisplayName;
            Description = property.Description;
            Filter = new BusinessObjectSchemaPropertyFilterResource(businessObject, property, request);
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public BusinessObjectSchemaPropertyFilterResource Filter { get; set; }
    }

    public record BusinessObjectSchemaPropertyFilterResource
    {
        public BusinessObjectSchemaPropertyFilterResource(IBusinessObject businessObject, BusinessObjectProperty property, HttpRequest request)
        {
            IsRequired = property.Filter.IsRequired;
            OptionsUrl = property.Filter.Options is not null
                ? $"{request.Scheme}://{request.Host}/api/local-native-provider/options/{businessObject.Name}/{property.Name}"
                : null;
        }

        public bool IsRequired { get; }
        public string OptionsUrl { get; }
    }
}
