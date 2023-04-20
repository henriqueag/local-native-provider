using System.Text.Json;

namespace LocalNativeProvider.BusinessObjects;

public class BusinessObjectFilter
{
    public string Operator { get; set; }
    public string Property { get; set; }
    public IEnumerable<object> Values { get; set; }
    public IEnumerable<BusinessObjectFilter> Conditions { get; set; }

    public object Value()
    {
        return Values?.First();
    }

    public void NormalizeValues(IEnumerable<BusinessObjectProperty> schema)
    {
        var valuesArray = Values as object[] ?? Values?.ToArray() ?? Array.Empty<object>();
        var schemaArray = schema as BusinessObjectProperty[] ?? schema.ToArray();

        if (!string.IsNullOrEmpty(Property))
        {
            var property = schemaArray.First(x => x.Name == Property);

            Values = valuesArray.Select(item =>
            {
                if (property.Type == "date" && item is JsonElement jsonElementDate)
                {
                    return DateTime.Parse(jsonElementDate.GetString()!);
                }

                if (property.Type == "string" && item is JsonElement jsonElementString)
                {
                    return jsonElementString.GetString();
                }

                if (property.Type == "number" && item is JsonElement jsonElementNumber)
                {
                    return jsonElementNumber.GetDecimal();
                }

                if (property.Type == "boolean" && item is JsonElement jsonElementBoolean)
                {
                    return jsonElementBoolean.GetBoolean();
                }

                return item;
            });
        }

        foreach (var condition in Conditions ?? Array.Empty<BusinessObjectFilter>())
        {
            condition.NormalizeValues(schemaArray);
        }
    }
}