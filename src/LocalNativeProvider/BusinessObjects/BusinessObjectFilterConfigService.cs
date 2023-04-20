using Dapper;
using LocalNativeProvider.Queries;
using Npgsql;
using SqlKata;

namespace LocalNativeProvider.BusinessObjects;

public class BusinessObjectFilterConfigService
{
    private readonly NpgsqlDataSource _dataSource;

    public BusinessObjectFilterConfigService(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public BusinessObjectFilterConfig GetBusinessObjectFilterConfig(string table, string distinctColumn)
    {
        using var connection = _dataSource.OpenConnection();

        var query = new Query(table)
            .Select(distinctColumn)
            .Distinct();

        var compiledQuery = QueryCompilerAcessor.Compiler.Compile(query);
        var dynamicResult = connection.Query(compiledQuery.Sql, compiledQuery.NamedBindings);
        var optionsResult = dynamicResult.Cast<IDictionary<string, object>>()
            .Select(x => new KeyValuePair<object, string>(x[distinctColumn], x[distinctColumn].ToString()))
            .ToArray();

        return new BusinessObjectFilterConfig(optionsResult);
    }
}