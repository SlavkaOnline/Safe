using TypedEnum.Tests.Data;


namespace TypedEnum.Tests;

public class SetTests
{
    [Fact]
    public void GetAll()
    {
        var all = TypedEnumFactory<TypedEnumSimple>.All;
        
        Assert.Collection(all, 
            val => Assert.Equal("1", val.Value),
            val => Assert.Equal("2", val.Value),
            val => Assert.Equal("3", val.Value)
        );
    }
    
    [Fact]
    public void GetValues()
    {
        var allSimple = TypedEnumFactory<TypedEnumSimple>.Values;
        
        Assert.Collection(allSimple, 
            val => Assert.Equal("1", val),
            val => Assert.Equal("2", val),
            val => Assert.Equal("3", val)
        );
        
        var allClass = TypedEnumFactory<TypedEnumClass>.Values;
        
        Assert.Collection(allClass, 
            val => Assert.Equal("1", val),
            val => Assert.Equal("2", val),
            val => Assert.Equal("3", val)
        );
    }
    
    
    [Fact]
    public void GetAllWithFilterValues()
    {
        var all = TypedEnumFactory<TypedEnumClass>.All;
        
        Assert.Collection(all, 
            val => Assert.Equal("1", val.Value),
            val => Assert.Equal("2", val.Value),
            val => Assert.Equal("3", val.Value)
        );
    }
}