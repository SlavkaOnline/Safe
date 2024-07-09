using Safe.Set;

namespace Safe.Tests.Data;

interface ITypedEnumInterface : ITypedEnum<ITypedEnumInterface>
{
    public static ITypedEnumInterface Undefined { get; } = Undef.Instance;
    public static ITypedEnumInterface One { get; } = new Ok("1");
    public static ITypedEnumInterface Two  { get; } = new Ok("2");
    public static ITypedEnumInterface Three  { get; } = new Ok("3");
        
    static TypedEnumSet<ITypedEnumInterface> ITypedEnum<ITypedEnumInterface>.Set => 
        TypedEnumSetBuilder.Create<ITypedEnumInterface>()
            .ReflectFromType(x => !ReferenceEquals(x, Undefined))
            .Build();

    private record Ok(string Value) : ITypedEnumInterface;
        
    private record Undef(): ITypedEnumInterface
    {
        public static Undef Instance { get; } = new ();
        public string Value => throw new Exception("Undefined");
    }
}