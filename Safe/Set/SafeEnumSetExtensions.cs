using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Safe.Set;

public static class SafeEnumSetExtensions
{
    public static bool TryParse<TEnum>(this SafeEnumSet<TEnum> set, string? value, [MaybeNullWhen(false)] out TEnum result)
        where TEnum : ISafeEnum<TEnum>
    {
        if (value is null)
        {
            result = default;
            return false;
        }

        return set.TryGetValue(value, out result);
    }

    public static TEnum Parse<TEnum>(this SafeEnumSet<TEnum> set, string? value)
        where TEnum : ISafeEnum<TEnum>
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
    ) where TEnum : ISafeEnum<TEnum>
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