namespace ResultLib;

public static class Result
{
    public static IResult Ok() => OkResult.Instance;
    
    public static IResult Err() => ErrResult.Instance;
    
    public static IResult<TOk> Ok<TOk>(TOk ok) => new OkResult<TOk>(ok);

    public static IResult<TOk> Err<TOk>() => UnitErrResult<TOk>.Instance;
    
    public static IUnitResult<TErr> Ok<TErr>() => UnitOkResult<TErr>.Instance;
    
    public static IUnitResult<TErr> Err<TErr>(TErr err) => new ErrResult<TErr>(err);
    
    public static IResult<TOk, TErr> Ok<TOk, TErr>(TOk ok) => new OkResult<TOk, TErr>(ok);
    
    public static IResult<TOk, TErr> Err<TOk, TErr>(TErr err) => new ErrResult<TOk, TErr>(err);
}