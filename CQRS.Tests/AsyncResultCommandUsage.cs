using Google.Rpc;
using ResultLib;


namespace CQRS.Tests;

public class AsyncResultCommandUsage
{
    public readonly record struct Context(int value);

    public interface IAppErr
    {
        public record NotFound(): IAppErr;
        
    }
    
    
    public class MyCommand : IAsyncCommand<Context, IResult<string, IAppErr>>
    {
        public async Task<IResult<string, IAppErr>> Execute(Context ctx, CancellationToken ct = default)
        {
            return Result.Err<string, IAppErr>(new IAppErr.NotFound());
        }
    }
    
    
}