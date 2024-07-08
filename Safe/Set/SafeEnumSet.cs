using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Safe.Set;

public class SafeEnumSet<TEnum> : IReadOnlyDictionary<string, TEnum>
    where TEnum : ISafeEnum<TEnum>
{
    private readonly ConcurrentDictionary<string, TEnum> _dictionary;

    public ImmutableArray<TEnum> All { get; }

    public SafeEnumSet(ConcurrentDictionary<string, TEnum> dictionary)
    {

        _dictionary = dictionary;
        if (dictionary.Any())
        {
            var val = dictionary.First();
            All = val.Value is IComparable<TEnum>
                ? dictionary.Select(x => x.Value).OrderBy(x => x, Comparer<TEnum>.Default).ToImmutableArray()
                : dictionary.Select(x => x.Value).ToImmutableArray();
        }
        else
        {
            All = ImmutableArray<TEnum>.Empty;
        }
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, TEnum>> GetEnumerator() => _dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _dictionary).GetEnumerator();

    /// <inheritdoc />
    public int Count => _dictionary.Count;

    /// <inheritdoc />
    public bool ContainsKey(string key) => _dictionary.ContainsKey(key);

    /// <inheritdoc />
    public bool TryGetValue(string key,
        [MaybeNullWhen(false)] out TEnum value
    ) => _dictionary.TryGetValue(key, out value);

    /// <inheritdoc />
    public TEnum this[string key] => _dictionary[key];

    /// <inheritdoc />
    public IEnumerable<string> Keys => _dictionary.Keys;

    /// <inheritdoc />
    public IEnumerable<TEnum> Values => _dictionary.Values;
}