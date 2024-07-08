using System.Text.Json;
using System.Text.Json.Serialization;

namespace Safe.Json;

public class SafeEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : ISafeEnum<TEnum>
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (!TEnum.TryParse(value, out var type))
        {
            throw new InvalidCastException($"Не удалось преобразовать значение {value} в тип {typeof(TEnum)}");
        }

        return type;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(JsonSerializer.Serialize(value.Value));
    }
}
