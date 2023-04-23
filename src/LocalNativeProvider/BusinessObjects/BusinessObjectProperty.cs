using System.ComponentModel;
using System.Reflection;

namespace LocalNativeProvider.BusinessObjects;

public record BusinessObjectProperty
{
    public BusinessObjectProperty(PropertyInfo propertyInfo)
    {
        Name = propertyInfo.Name;
        DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name;
        Description = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name;
        Type = PropertyType.FromType(propertyInfo.PropertyType).Name;
        Filter = BusinessObjectFilterConfig.Default;
    }

    public string Type { get; init; }
    public string Name { get; init; }
    public string DisplayName { get; init; }
    public string Description { get; init; }
    public BusinessObjectFilterConfig Filter { get; init; } 
}
