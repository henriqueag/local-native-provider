using System.Data.Common;

namespace LocalNativeProvider.BusinessObjects;

public class BusinessObjectFilterConfig
{
    public static readonly BusinessObjectFilterConfig Default = new(null);
    
    public BusinessObjectFilterConfig(IEnumerable<KeyValuePair<object, string>> filterOptions, bool isRequired = false)
    {
        IsRequired = isRequired;
        Options = filterOptions is not null && filterOptions.Any()
            ? filterOptions
            : null;
    }

    public bool IsRequired { get; init; }
    public IEnumerable<KeyValuePair<object, string>> Options { get; init; }
}