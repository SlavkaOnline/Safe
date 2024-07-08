using System.Collections.Concurrent;

namespace Safe.Set;

 public static class SafeEnumSetBuilder
    {
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>()
        where TEnum: ISafeEnum<TEnum>
            => new ();
 
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>(IEqualityComparer<string> comparer)
            where TEnum : ISafeEnum<TEnum> => new (comparer);
    }

    public class SafeEnumSetBuilder<TEnum>
        where TEnum : ISafeEnum<TEnum>
    {
        private readonly IEqualityComparer<string> _comparer;
        private readonly Dictionary<string, TEnum> _data;

        public SafeEnumSetBuilder() : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public SafeEnumSetBuilder(IEqualityComparer<string> comparer)
        {
            _comparer = comparer;
            _data = new Dictionary<string, TEnum>(comparer);
        }

        public SafeEnumSetBuilder<TEnum> Add(TEnum r)
        {
            _data.Add(r.Value, r);
            return this;
        }

        public SafeEnumSet<TEnum> Build() =>
            new(new ConcurrentDictionary<string, TEnum>(_data, _comparer));
    }