namespace CQRS;

public interface ICommand
{
    /// <summary>Выполнить команду</summary>
    void Execute();
}

/// <summary>Команда</summary>
public interface IReturningCommand<out TResult>
{
    /// <summary>Выполнить команду</summary>
    TResult Execute();
}

/// <summary>Команда</summary>
public interface ICommand<in TContext>
{
    /// <summary>Выполнить команду</summary>
    void Execute(TContext context);
}

/// <summary>Команда</summary>
public interface ICommand<in TContext, out TResult>
{
    /// <summary>Выполнить команду</summary>
    TResult Execute(TContext context);
}