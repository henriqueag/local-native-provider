using System.Data;

namespace LocalNativeProvider.BusinessObjects;

public interface IBusinessObject
{
    public string Name { get; }
    public string DisplayName { get; }
    public string Description { get; }
    
    public IEnumerable<BusinessObjectProperty> Properties { get; }

    Task<IEnumerable<object>> GetDataAsync(IDbConnection connection, BusinessObjectFilter filter, int skip, int take);

    void InitializeProperties(IServiceProvider services);
}