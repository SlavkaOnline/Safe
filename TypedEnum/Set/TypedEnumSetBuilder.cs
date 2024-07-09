using System.Collections.Concurrent;

namespace TypedEnum.Set;

 public static class TypedEnumSetBuilder
    {
        /// <summary>
        /// Созднать построитель с <see cref="StringComparer.OrdinalIgnoreCase"/> 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>()
        where TEnum: ITypedEnum<TEnum>
            => new ();
 
        /// <summary>
        /// Созднать построитель
        /// </summary>
        /// <param name="comparer">Сравнение значение</param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static SafeEnumSetBuilder<TEnum> Create<TEnum>(IEqualityComparer<string> comparer)
            where TEnum : ITypedEnum<TEnum> => new (comparer);
    }

    /// <summary>
    /// Строиель множенства перечисления
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
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

        /// <summary>
        /// Добавить значение
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public SafeEnumSetBuilder<TEnum> Add(TEnum r)
        {
            _data.Add(r.Value, r);
            return this;
        }

        /// <summary>
        /// Построить
        /// </summary>
        /// <returns></returns>
        public TypedEnumSet<TEnum> Build() =>
            new(new ConcurrentDictionary<string, TEnum>(_data, _comparer));
    }