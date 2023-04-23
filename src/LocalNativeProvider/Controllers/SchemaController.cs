using LocalNativeProvider.BusinessObjects;
using LocalNativeProvider.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LocalNativeProvider.Controllers;

[Route("api/local-native-provider/schema/{businessObjectName}")]
public class SchemaController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromRoute] string businessObjectName)
    {
        var businessObject = BusinessObjectList.Instance.FirstOrDefault(x => x.Name == businessObjectName);
        if (businessObject is null)
        {
            return NotFound();
        }

        var schema = new SchemaResource(businessObject, Request);

        return Ok(schema);
    }

    public record SchemaResource()
    {
        public SchemaResource(IBusinessObject businessObject, HttpRequest request) : this()
        {
            Name = businessObject.Name;
            DisplayName = businessObject.DisplayName;
            Description = businessObject.Description;

            SchemaUrl = $"{request.Scheme}://{request.Host}/api/local-native-provider/schema/{businessObject.Name}";
            DataUrl = $"{request.Scheme}://{request.Host}/api/local-native-provider/data/{businessObject.Name}";

            Properties = businessObject.Properties.Select(prop => new SchemaPropertyResource(businessObject, prop, request));
        }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public IEnumerable<string> Areas { get; } = BusinessObjectAreas.Value;
        public string SchemaUrl { get; }
        public string DataUrl { get; }
        public IEnumerable<SchemaPropertyResource> Properties { get; }
    }

    public record SchemaPropertyResource
    {
        public SchemaPropertyResource(IBusinessObject businessObject, BusinessObjectProperty property, HttpRequest request)
        {
            Type = property.Type;
            Name = property.Name;
            DisplayName = property.DisplayName;
            Description = property.Description;
            Filter = new SchemaPropertyFilterResource(businessObject, property, request);
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public SchemaPropertyFilterResource Filter { get; set; }
    }

    public record SchemaPropertyFilterResource
    {
        public SchemaPropertyFilterResource(IBusinessObject businessObject, BusinessObjectProperty property, HttpRequest request)
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