namespace LocalNativeProvider.BusinessObjects;

public record PropertyType
{
    public static readonly PropertyType String = new("string", string.Empty);
    public static readonly PropertyType Number = new("number", 0d);
    public static readonly PropertyType Date = new("date", DateTime.MinValue);
    public static readonly PropertyType Boolean = new("boolean", false);

    private PropertyType(string name, object clrValue)
    {
        Name = name;
        ClrValue = clrValue;
    }

    public string Name { get; }
    public object ClrValue { get; }

    public static PropertyType FromValue(object value)
    {
        return value is not null
            ? FromType(value.GetType())
            : String;
    }

    public static PropertyType FromType(Type type)
    {
        if (type == typeof(DateTimeOffset))
            return Date;

        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => Number,
            TypeCode.Int16 => Number,
            TypeCode.UInt16 => Number,
            TypeCode.Int32 => Number,
            TypeCode.UInt32 => Number,
            TypeCode.Int64 => Number,
            TypeCode.UInt64 => Number,
            TypeCode.Single => Number,
            TypeCode.Double => Number,
            TypeCode.Decimal => Number,

            TypeCode.DateTime => Date,

            TypeCode.Boolean => Boolean,

            _ => String,
        };
    }
}
