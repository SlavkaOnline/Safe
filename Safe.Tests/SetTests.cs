using System.Collections.Immutable;

namespace Safe.Tests;

public class SetTests
{
    
    class SafeEnum : ISafeEnum<SafeEnum>
    {
        public string Value { get; }

        private SafeEnum(string value)
        {
            Value = value;
        }

        public static SafeEnum One => new("1");
        public static SafeEnum Two => new("2");
        public static SafeEnum Three => new("3");
        
        public static ImmutableArray<SafeEnum> All => ISafeEnum<SafeEnum>.All;
    }
    
    [Fact]
    public void GetAll()
    {
        var all = SafeEnum.All;
        
        Assert.Collection(all, 
            val => Assert.Equal("1", val.Value),
            val => Assert.Equal("2", val.Value),
            val => Assert.Equal("3", val.Value)
        );
    }
}