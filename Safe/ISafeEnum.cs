using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Safe.Set;

namespace Safe;

public interface ISafeEnum<TEnum> : IValue<string, TEnum>, IComparable<TEnum>
where TEnum: ISafeEnum<TEnum>
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

    public static virtual SafeEnumSet<TEnum> Set => SafeEnumSetBuilder.Create<TEnum>()
        .ReflectFromType(x => true)
        .Build();

    static ImmutableArray<TEnum> All => TEnum.Set.All;
    
    int IComparable<TEnum>.CompareTo(TEnum? other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
}