namespace CQRS;

public interface IAsyncQuery<TOut>
{
    Task<TOut> Ask();
}

public interface IAsynQuery<in TSpecification, TOut>
{
    Task<TOut> Ask(TSpecification specification);
}