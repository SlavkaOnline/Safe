using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using TypedEnum.Set;

namespace TypedEnum;

public interface ITypedEnum<TEnum> : IValue<string, TEnum>, IComparable<TEnum>, IEquatable<TEnum>
where TEnum: ITypedEnum<TEnum>
{
    static TEnum IValue<string, TEnum>.Create(string? value)
    {
        if (!TEnum.TryParse(value, out var obj))
        {
            throw new ArgumentException("Не удалось распарсить значение", nameof(value));
        }

        return obj;
    }

    public static virtual bool TryParse(string? value, [NotNullWhen(true)] out TEnum? obj)
        => TEnum.Set.TryParse(value, out obj);

    public static virtual TypedEnumSet<TEnum> Set => TypedEnumSetBuilder.Create<TEnum>()
        .ReflectFromType(_ => true)
        .Build();

    static virtual ImmutableArray<TEnum> All => TEnum.Set.All;
    
    int IComparable<TEnum>.CompareTo(TEnum? other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
    bool IEquatable<TEnum>.Equals(TEnum? other) => !ReferenceEquals(other, null) && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
}