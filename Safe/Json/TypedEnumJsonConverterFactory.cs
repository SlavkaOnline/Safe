using System.Text.Json;
using System.Text.Json.Serialization;

namespace Safe.Json;

public class TypedEnumJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.GetInterfaces()
            .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypedEnum<>)) != null;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var safeEnum = typeToConvert.GetInterfaces()
            .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypedEnum<>));

        var enumType = safeEnum.GetGenericArguments()[0];

        var controllerType =  typeof(TypedEnumConverter<>).MakeGenericType(enumType);
        return (JsonConverter)Activator.CreateInstance(controllerType)!;
    }
}
