public class Result
{
    public bool IsSuccessful { get; init; }
    public ErrorType ErrorType { get; init; }
    public string? ErrorMessage { get; init; }
    protected Result(bool isSuccessful, string? erMessage = null, ErrorType erType = ErrorType.None)
        => (IsSuccessful, ErrorType, ErrorMessage) = (isSuccessful, erType, erMessage);

    public static Result Success() => new (true);
    public static Result Failure(ErrorType erType) => new (false, erType: erType);
    public static Result Failure(string message, ErrorType erType) => new (false, message, erType);
}

public class Result<T> : Result
{
    public T? Value { get; init; }

    private Result(bool isSuccessful, T value, string? erMessage = null, ErrorType erType = ErrorType.None) : base(isSuccessful, erMessage, erType)
    {
        Value = value;
    }

    public static Result<T> Success(T val) => new (true, val);
    public static Result<T> Failure(T val, ErrorType erType) => new (false, val, erType: erType);
    public static Result<T> Failure(T val, string message, ErrorType erType) => new (false, val, message, erType);
}