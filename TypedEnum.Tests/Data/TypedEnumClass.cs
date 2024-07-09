using TypedEnum.Set;

namespace TypedEnum.Tests.Data;

abstract class TypedEnumClass : ITypedEnum<TypedEnumClass>
{
    public abstract string Value { get; }
        
    public static TypedEnumClass Undefined => Undef.Instance;

    public static TypedEnumClass One { get; } = new Ok("1");
    public static TypedEnumClass Two  { get; } = new Ok("2");
    public static TypedEnumClass Three  { get; } = new Ok("3");
        
    public static TypedEnumSet<TypedEnumClass> Set => TypedEnumSetBuilder.Create<TypedEnumClass>()
        .ReflectFromType(x => !ReferenceEquals(x, Undefined))
        .Build();
    
    private class Ok(string value) : TypedEnumClass
    {
        public override string Value { get; } = value;
    }
        
    private class Undef(): TypedEnumClass
    {
        public static Undef Instance { get; } = new ();
        public override string Value => throw new Exception("Undefined");
    }
}