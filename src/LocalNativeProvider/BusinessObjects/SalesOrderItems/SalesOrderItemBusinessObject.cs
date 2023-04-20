using System.Data;
using System.Reflection;
using Dapper;
using LocalNativeProvider.Queries;
using SqlKata;

namespace LocalNativeProvider.BusinessObjects.SalesOrderItems;

public class SalesOrderItemBusinessObject : IBusinessObject
{
    public string Name { get; } = "local.bikestores.sales-orders-items";
    public string DisplayName { get; } = "Items dos pedidos de vendas";
    public string Description { get; } = "Lista com os items dos pedidos de vendas";

    public IEnumerable<BusinessObjectProperty> Properties { get; private set; }

    public async Task<IEnumerable<object>> GetDataAsync(IDbConnection connection, BusinessObjectFilter filter, int skip, int take)
    {
        var query = new Query(SalesOrderItemData.TableName)
            .ApplyFilter(filter, Properties)
            .Skip(skip)
            .Take(take);

        var compiledQuery = QueryCompilerAcessor.Compiler.Compile(query);
        var result = await connection.QueryAsync<SalesOrderItemData>(compiledQuery.Sql, compiledQuery.NamedBindings);

        return result;
    }

    public void InitializeProperties(IServiceProvider services)
    {
        Properties = typeof(SalesOrderItemData)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(propertyInfo => new BusinessObjectProperty(propertyInfo))
            .ToList();
    }
}
