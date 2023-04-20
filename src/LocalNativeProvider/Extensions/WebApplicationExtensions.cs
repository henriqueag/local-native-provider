using LocalNativeProvider.BusinessObjects;

namespace LocalNativeProvider.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitializeBusinessObjects(this WebApplication app)
        {
            foreach(var businessObject in BusinessObjectList.Instance)
            {
                businessObject.InitializeProperties(app.Services);
            }
        }
    }
}
