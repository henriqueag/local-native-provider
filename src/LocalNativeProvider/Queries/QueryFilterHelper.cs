using LocalNativeProvider.BusinessObjects;
using SqlKata;

namespace LocalNativeProvider.Queries;

public static class QueryFilterHelper
{
    public static Query ApplyFilter(this Query query, BusinessObjectFilter filter, IEnumerable<BusinessObjectProperty> schema)
    {
        if (filter is not null)
        {
            filter.NormalizeValues(schema);
            return ApplyAndFilter(query, filter);
        }

        return query;
    }

    private static Query ApplyAndFilter(this Query query, BusinessObjectFilter filter)
    {
        return filter.Operator switch
        {
            "And" => query.Where(and => filter.Conditions.Aggregate(and, ApplyAndFilter)),
            "Or" => query.Where(or => filter.Conditions.Aggregate(or, ApplyOrFilter)),

            "In" => query.WhereIn(filter.Property, filter.Values),
            "NotIn" => query.WhereNotIn(filter.Property, filter.Values),
            "Between" => query.WhereBetween(filter.Property, filter.Values.First(), filter.Values.Last()),

            "LessThan" => query.Where(filter.Property, "<", filter.Value()),
            "GreaterThan" => query.Where(filter.Property, ">", filter.Value()),
            "LessOrEquals" => query.Where(filter.Property, "<=", filter.Value()),
            "GreaterOrEquals" => query.Where(filter.Property, ">=", filter.Value()),
            "Equal" => query.Where(filter.Property, filter.Value()),
            "NotEqual" => query.WhereNot(filter.Property, filter.Value()),
            "IsNull" => query.WhereNull(filter.Property),
            "IsNotNull" => query.WhereNotNull(filter.Property),
            "Like" => query.WhereLike(filter.Property, filter.Value()),
            "NotLike" => query.WhereNotLike(filter.Property, filter.Value()),

            _ => throw new NotSupportedException($"O operador {filter.Operator} não foi implementado")
        };
    }

    private static Query ApplyOrFilter(this Query query, BusinessObjectFilter filter)
    {
        return filter.Operator switch
        {
            "And" => query.OrWhere(and => filter.Conditions.Aggregate(and, ApplyAndFilter)),
            "Or" => query.OrWhere(or => filter.Conditions.Aggregate(or, ApplyOrFilter)),

            "In" => query.OrWhereIn(filter.Property, filter.Values),
            "NotIn" => query.OrWhereNotIn(filter.Property, filter.Values),
            "Between" => query.OrWhereBetween(filter.Property, filter.Values.First(), filter.Values.Last()),

            "LessThan" => query.OrWhere(filter.Property, "<", filter.Value()),
            "GreaterThan" => query.OrWhere(filter.Property, ">", filter.Value()),
            "LessOrEquals" => query.OrWhere(filter.Property, "<=", filter.Value()),
            "GreaterOrEquals" => query.OrWhere(filter.Property, ">=", filter.Value()),
            "Equal" => query.OrWhere(filter.Property, filter.Value()),
            "NotEqual" => query.OrWhereNot(filter.Property, filter.Value()),
            "IsNull" => query.OrWhereNull(filter.Property),
            "IsNotNull" => query.OrWhereNotNull(filter.Property),
            "Like" => query.OrWhereLike(filter.Property, filter.Value()),
            "NotLike" => query.OrWhereNotLike(filter.Property, filter.Value()),

            _ => throw new NotSupportedException($"O operador {filter.Operator} não foi implementado")
        };
    }
}