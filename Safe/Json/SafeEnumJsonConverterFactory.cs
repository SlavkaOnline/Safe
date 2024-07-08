using System.Text.Json;
using System.Text.Json.Serialization;

namespace Safe.Json;

public class SafeEnumJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.GetInterfaces()
            .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISafeEnum<>)) != null;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var safeEnum = typeToConvert.GetInterfaces()
            .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISafeEnum<>));

        var enumType = safeEnum.GetGenericArguments()[0];

        var controllerType =  typeof(SafeEnumConverter<>).MakeGenericType(enumType);
        return (JsonConverter)Activator.CreateInstance(controllerType)!;
    }
}
