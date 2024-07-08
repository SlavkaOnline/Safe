using MongoDB.Bson.Serialization;

namespace Safe.Bson;

public class SafeEnumBsonSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer? GetSerializer(Type type)
    {
        var safeEnum = type.GetInterfaces()
            .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISafeEnum<>));

        if (safeEnum == null)
            return null;

        var enumType = safeEnum.GetGenericArguments()[0];

        var controllerType = typeof(SafeEnumBsonSerializer<>).MakeGenericType(enumType);
        return (IBsonSerializer) Activator.CreateInstance(controllerType)!;
    }
}