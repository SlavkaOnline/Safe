using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TypedEnum.Bson;
using TypedEnum.Tests.Data;

namespace TypedEnum.Tests;

public class TypedEnumBsonSerializerTests
{
    record ModelWithInterface(ITypedEnumInterface EValue);

    public TypedEnumBsonSerializerTests()
    {
        BsonSerializer.RegisterSerializationProvider(new TypedEnumBsonSerializationProvider());
    }


    [Fact]
    public void Deserialize()
    {
        var str = """
                  {
                    "EValue": "2"
                  }
                  """;

        BsonDocument.TryParse(str, out var bson);

        BsonSerializer.RegisterSerializationProvider(new TypedEnumBsonSerializationProvider());
        var model = BsonSerializer.Deserialize<ModelWithInterface>(bson);

        Assert.Equal("2", model.EValue.Value);
    }

    [Fact]
    public void Serialize()
    {
        var bson = new BsonDocument();
        BsonSerializer.Serialize(new BsonDocumentWriter(bson), typeof(ModelWithInterface), new ModelWithInterface(ITypedEnumInterface.Two));
        
        Assert.Equal("2", bson.Elements.First(x => x.Name == "EValue").Value);
    }

    [Fact]
    public void BuilderEqFilter()
    {
        var filter = Builders<ModelWithInterface>.Filter.Eq(x => x.EValue, ITypedEnumInterface.Three);

        var json = ConvertFilterToJson(filter);

        var str = """{ "EValue" : "3" }""";
        
        Assert.Equal(str, json);
    }
    
    [Fact]
    public void BuilderInFilter()
    {
        var filter = Builders<ModelWithInterface>.Filter.In(x => x.EValue, [ITypedEnumInterface.Three, ITypedEnumInterface.Two]);
        var json = ConvertFilterToJson(filter);

        var str = """{ "EValue" : { "$in" : ["3", "2"] } }""";
        
        Assert.Equal(str, json);
    }
    
    private static string ConvertFilterToJson<T>(FilterDefinition<T> filter)
    {
        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<T>();
        return filter.Render(documentSerializer, serializerRegistry).ToJson();
    }
}