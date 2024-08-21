namespace Result.Tests;

using ResultLib;

public class ResultTests
{

    [Fact]
    public void TestEmptyOkResult()
    {
        var result = Result.Ok();

        if (result.IsOk(out var ok, out var err))
        {
            Assert.IsAssignableFrom<IOkResult>(ok);
        }

        Assert.Null(err);
    }

    [Fact]
    public void TestEmptyErrResult()
    {
        var resultErr = Result.Err();
        
        if (resultErr.IsErr(out var ok, out var err))
        {
            Assert.IsAssignableFrom<IErrResult>(err);
        }
        
        Assert.Null(ok);
    }

    [Fact]
    public void TestOkResult()
    {
        var result = Result.Ok("OK");

        if (result.IsOk(out var ok, out var err))
        {
            Assert.Equal("OK", ok);
        }
        
        Assert.Null(err);
    }
    
    [Fact]
    public void TestUnitErrResult()
    {
        var result = Result.Err<string>();

        if (result.IsErr(out var ok, out var err))
        {
            Assert.IsAssignableFrom<IUnitErrResult<string>>(err);
        }
        
        Assert.Null(ok);
    }
    
    [Fact]
    public void TestUnitOkResult()
    {
        var result = Result.Ok<string>();

        if (result.IsOk(out var ok, out var err))
        {
            Assert.IsAssignableFrom<IUnitOkResult<string>>(ok);
        }
        
        Assert.Null(err);
    }
    
    [Fact]
    public void TestErrResult()
    {
        var result = Result.Err<string>("err");

        if (result.IsErr(out var ok, out var err))
        {
            Assert.Equal("err", err);
        }
        
        Assert.Null(ok);
    }

    [Fact]
    public void TestOkOkErrResult()
    {
        var result = Result.Ok<string, string>("123");
        
        if (result.IsOk(out var ok, out var err))
        {
            Assert.Equal("123", ok);
        }
        
        Assert.Null(err);
    }
    
    [Fact]
    public void TestErrOkErrResult()
    {
        var result = Result.Err<string, string>("err");
        
        if (result.IsErr(out var ok, out var err))
        {
            Assert.Equal("err", err);
        }
        
        Assert.Null(ok);
    }
    
}