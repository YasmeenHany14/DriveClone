namespace DriveClone.Application.Common.ErrorAndResults;

//TODO: Make them one class if possible
public class Result : IResult
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private Result(T value) : base(true, Error.None) => Value = value; // success ctor
    private Result(Error error) : base(false, error) => Value = default; // failure ctor
    
    public T? Value { get; }
    
    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(Error error) => new(error);
}
