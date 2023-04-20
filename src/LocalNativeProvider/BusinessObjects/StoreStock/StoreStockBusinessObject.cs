using System.Data;
using System.Reflection;
using Dapper;
using LocalNativeProvider.BusinessObjects.SalesOrder;
using LocalNativeProvider.Queries;
using SqlKata;

namespace LocalNativeProvider.BusinessObjects.StoreStock;

public class StoreStockBusinessObject : IBusinessObject
{
    public string Name { get; } = "local.bikestores.store-stock";
    public string DisplayName { get; } = "Estoque de loja";
    public string Description { get; } = "Representa o estoque disponível de produtos em uma loja";

    public IEnumerable<BusinessObjectProperty> Properties { get; private set; }

    public async Task<IEnumerable<object>> GetDataAsync(IDbConnection connection, BusinessObjectFilter filter, int skip, int take)
    {
        var query = new Query(StoreStockData.TableName)
            .ApplyFilter(filter, Properties)
            .Skip(skip)
            .Take(take);

        var compiledQuery = QueryCompilerAcessor.Compiler.Compile(query);
        var result = await connection.QueryAsync<StoreStockData>(compiledQuery.Sql, compiledQuery.NamedBindings);

        return result;
    }

    public void InitializeProperties(IServiceProvider services)
    {
        var filterConfigService = services.GetRequiredService<BusinessObjectFilterConfigService>();

        Properties = typeof(StoreStockData)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(propertyInfo => 
                new BusinessObjectProperty(propertyInfo)
                {
                    Filter = propertyInfo.Name switch
                    {
                        "StoreName" => filterConfigService.GetBusinessObjectFilterConfig(StoreStockData.TableName, "StoreName"),
                        _ => default
                    }
                })
            .ToList();
    }
}
