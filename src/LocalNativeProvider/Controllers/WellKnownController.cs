using Microsoft.AspNetCore.Mvc;

namespace LocalNativeProvider.Controllers;

[Route("api/local-native-provider/.well-known/bikestores/connector")]
public class WellKnownController : ControllerBase
{
    [HttpGet]
    public DiscoveryResource Get()
    {
        return new DiscoveryResource(Request.Scheme, Request.Host.ToString());
    }

    public record DiscoveryResource
    {
        public DiscoveryResource(string protocol, string host)
        {
            ObjectsUrl = $"{protocol}://{host}/api/local-native-provider/objects";
            SchemaUrl = $"{protocol}://{host}/api/local-native-provider/schema/{{bussiness-object-name}}";
            DataUrl = $"{protocol}://{host}/api/local-native-provider/data/{{bussiness-object-name}}";
        }

        public string ObjectsUrl { get; }
        public string SchemaUrl { get; }
        public string DataUrl { get; }
    }
}
