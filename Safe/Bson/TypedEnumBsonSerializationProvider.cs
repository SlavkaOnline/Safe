using MongoDB.Bson.Serialization;

namespace Safe.Bson;

public class TypedEnumBsonSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer? GetSerializer(Type type)
    {
        var safeEnum = type.GetInterfaces()
            .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypedEnum<>));

        if (safeEnum == null)
            return null;

        var enumType = safeEnum.GetGenericArguments()[0];

        var controllerType = typeof(TypedEnumBsonSerializer<>).MakeGenericType(enumType);
        return (IBsonSerializer) Activator.CreateInstance(controllerType)!;
    }
}