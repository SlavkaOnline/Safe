namespace TypedEnum.Tests.Data;

public class TypedEnumSimple : ITypedEnum<TypedEnumSimple>
{
    private TypedEnumSimple(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static TypedEnumSimple One { get; } = new("1");
    public static TypedEnumSimple Two { get; } = new("2");
    public static TypedEnumSimple Three { get; } = new("3");
}