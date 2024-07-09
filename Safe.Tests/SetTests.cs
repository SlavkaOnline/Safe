using System.Collections.Immutable;

namespace Safe.Tests;

public class SetTests
{
    
    class TypedEnum : ITypedEnum<TypedEnum>
    {
        public string Value { get; }

        private TypedEnum(string value)
        {
            Value = value;
        }

        public static TypedEnum One => new("1");
        public static TypedEnum Two => new("2");
        public static TypedEnum Three => new("3");
        
        public static ImmutableArray<TypedEnum> All => ITypedEnum<TypedEnum>.All;
    }
    
    [Fact]
    public void GetAll()
    {
        var all = TypedEnum.All;
        
        Assert.Collection(all, 
            val => Assert.Equal("1", val.Value),
            val => Assert.Equal("2", val.Value),
            val => Assert.Equal("3", val.Value)
        );
    }
}