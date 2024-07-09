namespace TypedEnum;

public interface IValue<TValue, out TObject>
{
    public TValue Value { get; }

    public static abstract TObject Create(TValue value);
}