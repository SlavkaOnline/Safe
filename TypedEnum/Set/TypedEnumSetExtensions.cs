using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TypedEnum.Set;

public static class TypedEnumSetExtensions
{
    /// <summary>
    /// Попробовать получить значение из множества
    /// </summary>
    /// <param name="set"></param>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
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

    /// <summary>
    /// Получить значение из множества
    /// </summary>
    /// <param name="set"></param>
    /// <param name="value"></param>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static TEnum Parse<TEnum>(this TypedEnumSet<TEnum> set, string? value)
        where TEnum : ITypedEnum<TEnum>
    {
        if (!set.TryParse(value, out var type))
        {
            throw new FormatException($"Не удалось преобразовать занчение {value} в тип {typeof(TEnum).FullName}");
        }

        return type;
    }
    
    /// <summary>
    /// Добавить все публичные статичесике поля в перечислении в построитель с исклучением из списка
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="include">Условие исклчения из списка</param>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
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