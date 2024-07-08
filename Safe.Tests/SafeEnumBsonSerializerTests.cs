using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Safe.Bson;

namespace Safe.Tests;

public class SafeEnumBsonSerializerTests
{
    struct SafeEnum : ISafeEnum<SafeEnum>
    {
        public string Value { get; }

        private SafeEnum(string value)
        {
            Value = value;
        }

        public static SafeEnum One => new("1");
        public static SafeEnum Two => new("2");
        public static SafeEnum Three => new("3");
    }

    record Model(SafeEnum EValue);

    public SafeEnumBsonSerializerTests()
    {
        BsonSerializer.RegisterSerializationProvider(new SafeEnumBsonSerializationProvider());
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

        BsonSerializer.RegisterSerializationProvider(new SafeEnumBsonSerializationProvider());

        var model = BsonSerializer.Deserialize<Model>(bson);

        Assert.Equal("2", model.EValue.Value);
    }

    [Fact]
    public void Serialize()
    {
        var bson = new BsonDocument();
        BsonSerializer.Serialize(new BsonDocumentWriter(bson), typeof(Model), new Model(SafeEnum.Two));
        Assert.Equal("2", bson.Elements.First(x => x.Name == "EValue").Value);
    }
}