using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Safe.Set;

public static class TypedEnumSetExtensions
{
    public static bool TryParse<TEnum>(this TypedEnumSet<TEnum> set, string? value, [MaybeNullWhen(false)] out TEnum result)
        where TEnum : ITypedEnum<TEnum>
    {
        if (value is null)
        {
            result = default;
            return false;
        }

        return set.TryGetValue(value, out result);
    }

    public static TEnum Parse<TEnum>(this TypedEnumSet<TEnum> set, string? value)
        where TEnum : ITypedEnum<TEnum>
    {
        if (!set.TryParse(value, out var type))
        {
            throw new FormatException($"Invalid {typeof(TEnum).Name}: {value}");
        }

        return type;
    }
    
    
    public static SafeEnumSetBuilder<TEnum> ReflectFromType<TEnum>(
        this SafeEnumSetBuilder<TEnum> builder,
        Func<TEnum, bool> include
    ) where TEnum : ITypedEnum<TEnum>
    {
        var all = typeof(TEnum).GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => typeof(TEnum).IsAssignableFrom(p.PropertyType))
            .Select(p => p.GetValue(null))
            .Cast<TEnum>()
            .Where(include);
        
        foreach (var val in all)
        {
            builder.Add(val);
        }

        return builder;
    }
}