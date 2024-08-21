namespace CQRS;

public interface IQuery<out TOut>
{
    TOut Ask();
}

public interface IQuery<in TSpecification, out TOut>
{
    TOut Ask(TSpecification specification);
}