using System.Text.Json;
using TypedEnum.Json;
using TypedEnum.Set;
using TypedEnum.Tests.Data;

namespace TypedEnum.Tests;

public class TypedEnumConverterTests
{
    record ModelWithClass(TypedEnumClass EValue);
    record ModelWithInterface(ITypedEnumInterface EValue);
    
    [Fact]
    public void DeserializeClass()
    {
        var str = """
                  {
                    "EValue": "2"
                  }
                  """;

        var options = new JsonSerializerOptions();
        options.Converters.Add(new TypedEnumJsonConverterFactory());

        var model = JsonSerializer.Deserialize<ModelWithClass>(str, options);

        Assert.Equal("2", model!.EValue.Value);
    }
    
    [Fact]
    public void SerializeClass()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TypedEnumJsonConverterFactory());

        var json = JsonSerializer.Serialize(new ModelWithInterface(ITypedEnumInterface.Three), options);

        Assert.Equal("""{"EValue":"3"}""", json);
    }
    
    [Fact]
    public void DeserializeInterface()
    {
        var str = """
                  {
                    "EValue": "2"
                  }
                  """;

        var options = new JsonSerializerOptions();
        options.Converters.Add(new TypedEnumJsonConverterFactory());

        var model = JsonSerializer.Deserialize<ModelWithInterface>(str, options);

        Assert.Equal("2", model!.EValue.Value);
    }
    
    [Fact]
    public void SerializeInterface()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TypedEnumJsonConverterFactory());

        var json = JsonSerializer.Serialize(new ModelWithInterface(ITypedEnumInterface.Three), options);

        Assert.Equal("""{"EValue":"3"}""", json);
    }
}