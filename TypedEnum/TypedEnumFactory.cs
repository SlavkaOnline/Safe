using System.Collections.Immutable;

namespace TypedEnum;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public static class TypedEnumFactory<TEnum>
    where TEnum : ITypedEnum<TEnum>
{
    public static ImmutableArray<string> Values { get; } = [..TEnum.All.Select(x => x.Value)];
    public static ImmutableArray<TEnum> All => TEnum.All;
}