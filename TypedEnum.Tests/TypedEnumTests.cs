using TypedEnum.Tests.Data;

namespace TypedEnum.Tests;

public class TypedEnumTests
{
    [Fact]
    public void EqualTest()
    {
        Assert.Equal(TypedEnumClass.Two, TypedEnumClass.Two);
        Assert.True(TypedEnumClass.Two == TypedEnumClass.Two);
        
        Assert.Equal(ITypedEnumInterface.Two, ITypedEnumInterface.Two);
        Assert.True(ITypedEnumInterface.Two == ITypedEnumInterface.Two);
        
        Assert.NotEqual(TypedEnumClass.One, TypedEnumClass.Two);
        Assert.True(TypedEnumClass.One != TypedEnumClass.Two);
        
        Assert.NotEqual(ITypedEnumInterface.One, ITypedEnumInterface.Two);
        Assert.True(ITypedEnumInterface.One != ITypedEnumInterface.Two);
    }
}