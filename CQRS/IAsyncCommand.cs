namespace CQRS;

public interface IAsyncCommand
{
    Task Execute(CancellationToken ct = default);
}

public interface IAsyncReturningCommand<TResult>
{
    Task<TResult> Execute(CancellationToken ct = default);
}

public interface IAsyncCommand<in TContext>
{
    Task Execute(TContext ctx, CancellationToken ct = default);
}

public interface IAsyncCommand<in TContext, TResult>
{
    /// <summary>Выполнить команду</summary>
    Task<TResult> Execute(TContext ctx, CancellationToken ct = default);
}
    