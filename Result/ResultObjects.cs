namespace Result;

public interface IResult;

public interface IResult<TResult>: IResult;

//Пустой резалт

public interface IOkResult : IResult;
public interface IErrResult : IResult;

public class OkResult : IOkResult
{
    private OkResult()
    {
        
    }

    public static OkResult Instance { get; } = new();
}

public class ErrResult : IErrResult
{
    private ErrResult()
    {
        
    }   
    public static ErrResult Instance { get; } = new();
}


//Резалт с пустой ошибкой

public interface IOkResult<TOk> : IOkResult, IResult<TOk>
{
    TOk Ok { get; }
} 

public interface IUnitErrResult<TOk> : IErrResult, IResult<TOk>;

public class OkResult<TOk> : IOkResult<TOk>, IErrResult 
{
    public OkResult(TOk ok)
    {
        Ok = ok;
    }
    public TOk Ok { get; }
}

public class UnitErrResult<TOk> : IUnitErrResult<TOk>
{
    private UnitErrResult()
    {
        
    }

    public static UnitErrResult<TOk> Instance { get; } = new ();
}


//Резалт с ошибкой, но пустым ответом 

public interface IUnitResult<TErr> : IResult;

public interface IUnitOkResult<TErr> : IOkResult, IUnitResult<TErr>;

public interface IErrResult<TErr> : IUnitResult<TErr>
{
    TErr Err { get; }
}

public class UnitOkResult<TErr> : IUnitOkResult<TErr>
{
    private UnitOkResult()
    {
        
    }
    
    public static UnitOkResult<TErr> Instance { get; } = new ();
}

public class ErrResult<TErr> : IErrResult<TErr>
{
    public ErrResult(TErr err)
    {
        Err = err;
    }
    
    public TErr Err { get; }
} 


//Резалт

public interface IResult<TOk, TErr> : IResult;

public interface IOkResult<TOk, TErr> : IOkResult<TOk>, IResult<TOk, TErr>;

public interface IErrResult<TOk, TErr> : IErrResult<TErr>, IResult<TOk, TErr>;


public class OkResult<TOk, TErr> : IOkResult<TOk, TErr>
{
    public OkResult(TOk ok)
    {
        Ok = ok;
    }
    public TOk Ok { get; }
}

public class ErrResult<TOk, TErr> : IErrResult<TOk, TErr>
{
    public ErrResult(TErr err)
    {
        Err = err;
    }
    public TErr Err { get; }
}

