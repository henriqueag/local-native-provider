using System.Data;
using System.Reflection;
using Dapper;
using LocalNativeProvider.Queries;
using SqlKata;

namespace LocalNativeProvider.BusinessObjects.SalesOrder;

public class SalesOrderBusinessObject : IBusinessObject
{
    public string Name { get; } = "local.bikestores.sales-orders";
    public string DisplayName { get; } = "Pedidos de vendas";
    public string Description { get; } = "Lista dos pedidos de vendas";

    public IEnumerable<BusinessObjectProperty> Properties { get; private set; }

    public async Task<IEnumerable<object>> GetDataAsync(IDbConnection connection, BusinessObjectFilter filter, int skip, int take)
    {
        var query = new Query(SalesOrderData.TableName)
            .ApplyFilter(filter, Properties)
            .Skip(skip)
            .Take(take);

        var compiledQuery = QueryCompilerAcessor.Compiler.Compile(query);
        var result = await connection.QueryAsync<SalesOrderData>(compiledQuery.Sql, compiledQuery.NamedBindings);

        return result;
    }

    public void InitializeProperties(IServiceProvider services)
    {
        var filterConfigService = services.GetRequiredService<BusinessObjectFilterConfigService>();

        Properties = typeof(SalesOrderData)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(propertyInfo =>
                new BusinessObjectProperty(propertyInfo)
                {
                    Filter = propertyInfo.Name switch
                    {
                        "OrderStatus" => filterConfigService.GetBusinessObjectFilterConfig(SalesOrderData.TableName, "OrderStatus"),
                        "StaffName" => filterConfigService.GetBusinessObjectFilterConfig(SalesOrderData.TableName, "StaffName"),
                        _ => default
                    }
                })
            .ToList();
    }
}