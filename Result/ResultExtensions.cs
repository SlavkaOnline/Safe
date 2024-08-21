using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Result;

public static class ResultExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk(
        this IResult result,
        [NotNullWhen(true)] out IOkResult? ok,
        [NotNullWhen(false)] out IErrResult? err)
    {
        switch (result)
        {
            case IOkResult okResult:
                ok = okResult;
                err = default;
                return true;
            case IErrResult errResult:
                err = errResult;
                ok = default;
                return false;
           default: 
               throw new InvalidOperationException(
                   $"Тип {result.GetType()} не является валидным типом результата " +
                   $"- он не реализует ни {typeof(IOkResult)} ни {typeof(IErrResult)}"
                   );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk(
        this IResult result,
        [NotNullWhen(true)] out IOkResult? ok
    ) => result.IsOk(out ok, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk(
        this IResult result
    ) => result.IsOk(out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr(
        this IResult result,
        [NotNullWhen(false)] out IOkResult? ok,
        [NotNullWhen(true)] out IErrResult? err)
        => !IsOk(result, out ok, out err);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr(
        this IResult result,
        [NotNullWhen(true)] out IErrResult? error
    ) => result.IsErr(out _, out error);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr(
        this IResult result
    ) => result.IsErr(out _, out _);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk>(
        this IResult<TOk> result,
        [MaybeNullWhen(false)] out TOk ok,
        [NotNullWhen(false)] out IUnitErrResult<TOk>? err)
    {
        switch (result)
        {
            case IOkResult<TOk>  okResult:
                ok = okResult.Ok;
                err = default;
                return true;
            case IUnitErrResult<TOk> errResult:
                err = errResult;
                ok = default;
                return false;
            case null:
                throw new ArgumentNullException(nameof(result));
            default: 
                throw new InvalidOperationException(
                    $"Тип {result.GetType()} не является валидным типом результата " +
                    $"- он не реализует ни {typeof(IOkResult<TOk>)} ни {typeof(IUnitErrResult<TOk>)}"
                );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk>(
        this IResult<TOk> result,
        [MaybeNullWhen(false)] out TOk ok) 
    => IsOk(result, out ok, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk>(
        this IResult<TOk> result)
        => IsOk(result, out _, out _);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TOk>(
        this IResult<TOk> result,
        [NotNullWhen(false)] out TOk? ok,
        [NotNullWhen(true)] out IUnitErrResult<TOk>? err)
        => !IsOk(result, out ok, out err);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TOk>(
        this IResult<TOk> result)
        => !IsOk(result, out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TErr>(
        this IUnitResult<TErr> result,
        [MaybeNullWhen(false)] out IOkResult? ok,
        [MaybeNullWhen(true)] out TErr? err)
    {
        switch (result)
        {
            case IOkResult okResult:
                ok = okResult;
                err = default;
                return true;
            case IErrResult<TErr> errResult:
                err = errResult.Err;
                ok = default;
                return false;
            case null:
                throw new ArgumentNullException(nameof(result));
            default: 
                throw new InvalidOperationException(
                    $"Тип {result.GetType()} не является валидным типом результата " +
                    $"- он не реализует ни {typeof(IOkResult)} ни {typeof(IErrResult<TErr>)}"
                );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TErr>(
        this IUnitResult<TErr> result,
        [MaybeNullWhen(false)] out IOkResult? ok,
        [NotNullWhen(true)] out TErr? err)
    => !IsOk(result, out ok, out err);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TErr>(
        this IUnitResult<TErr> result)
        => IsOk(result, out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TErr>(
        this IUnitResult<TErr> result,
        [NotNullWhen(true)] out TErr? err)
        => !IsOk(result, out _, out err);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TErr>(
        this IUnitResult<TErr> result)
        => !IsOk(result, out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk, TErr>(
        this IResult<TOk, TErr> result,
        [MaybeNullWhen(false)] out TOk? ok,
        [MaybeNullWhen(true)] out TErr? err)
    {
        switch (result)
        {
            case IOkResult<TOk, TErr> okResult:
                ok = okResult.Ok;
                err = default;
                return true;
            case IErrResult<TOk, TErr> errResult:
                err = errResult.Err;
                ok = default;
                return false;
            case null:
                throw new ArgumentNullException(nameof(result));
            default: 
                throw new InvalidOperationException(
                    $"Тип {result.GetType()} не является валидным типом результата " +
                    $"- он не реализует ни {typeof(IOkResult<TOk, TErr>)} ни {typeof(IErrResult<TOk, TErr>)}"
                );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TOk, TErr>(
        this IResult<TOk, TErr> result,
        [NotNullWhen(false)] out TOk? ok,
        [NotNullWhen(true)] out TErr? err)
     => !IsOk(result, out ok, out err);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk, TErr>(
        this IResult<TOk, TErr> result,
        [NotNullWhen(true)] out TOk? ok)
     => IsOk(result, out ok, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOk<TOk, TErr>(
        this IResult<TOk, TErr> result)
        => IsOk(result, out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TOk, TErr>(
        this IResult<TOk, TErr> result,
        [NotNullWhen(false)] out TOk? ok)
        => !IsOk(result, out ok, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsErr<TOk, TErr>(
        this IResult<TOk, TErr> result)
        => !IsOk(result, out _, out _);
}