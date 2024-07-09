using System.Text.Json;
using System.Text.Json.Serialization;

namespace TypedEnum.Json;

public class TypedEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : ITypedEnum<TEnum>
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (!TEnum.TryParse(value, out var type))
        {
            throw new FormatException($"Не удалось преобразовать значение {value} в тип {typeof(TEnum)}");
        }

        return type;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(JsonSerializer.Serialize(value.Value));
    }
}
