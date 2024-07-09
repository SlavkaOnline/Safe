using System.Collections.Concurrent;

namespace Safe.Set;

 public static class TypedEnumSetBuilder
    {
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>()
        where TEnum: ITypedEnum<TEnum>
            => new ();
 
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>(IEqualityComparer<string> comparer)
            where TEnum : ITypedEnum<TEnum> => new (comparer);
    }

    public class SafeEnumSetBuilder<TEnum>
        where TEnum : ITypedEnum<TEnum>
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

        public TypedEnumSet<TEnum> Build() =>
            new(new ConcurrentDictionary<string, TEnum>(_data, _comparer));
    }