using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using TypedEnum.Set;

namespace TypedEnum;

/// <summary>
/// Типизированное перечисление
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public interface ITypedEnum<TEnum> : IValue<string, TEnum>, IComparable<TEnum>, IEquatable<TEnum>
where TEnum: ITypedEnum<TEnum>
{
    /// <summary>
    /// Создать значение типизированного исклчения
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    static TEnum IValue<string, TEnum>.Create(string? value)
    {
        if (!TEnum.TryParse(value, out var obj))
        {
            throw new FormatException($"Не удалось преобразовать значение {value} в тип {typeof(TEnum)}");
        }

        return obj;
    }

    /// <summary>
    /// Попробовать создать значение типизированного исклчения
    /// </summary>
    /// <param name="value"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static virtual bool TryParse(string? value, [NotNullWhen(true)] out TEnum? obj)
        => TEnum.Set.TryParse(value, out obj);

    /// <summary>
    /// Множество значений
    /// </summary>
    public static virtual TypedEnumSet<TEnum> Set => TypedEnumSetBuilder.Create<TEnum>()
        .ReflectFromType(_ => true)
        .Build();

    /// <summary>
    /// Все значения
    /// </summary>
    static virtual ImmutableArray<TEnum> All => TEnum.Set.All;
    
    /// <inheritdoc />
    int IComparable<TEnum>.CompareTo(TEnum? other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
    
    /// <inheritdoc />
    bool IEquatable<TEnum>.Equals(TEnum? other) => !ReferenceEquals(other, null) && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
}