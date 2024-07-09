using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Safe.Bson;

public class TypedEnumBsonSerializer<TEnum> :  IBsonSerializer<TEnum>
    where TEnum : ITypedEnum<TEnum>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return BsonSerializer.Deserialize<TEnum>(context.Reader);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
    {
        context.Writer.WriteString(value.Value);
    }

    public TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.String)
        {
            var value = context.Reader.ReadString();
            if (!TEnum.TryParse(value, out var type))
            {
                throw new InvalidCastException($"Не удалось преобразовать значение {value} в тип {typeof(TEnum)}");
            }

            return type;
        }
        
        throw new InvalidOperationException($"{args.NominalType.FullName} cannot be deserialized");
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        Serialize(context, args, (TEnum)value);
    }

    public Type ValueType => typeof(TEnum);
}
