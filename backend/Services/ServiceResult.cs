namespace backend.Services;

public class ServiceResult<T>
{
    private ServiceResult(T? data, bool success, ServiceErrorType? errorType, string? errorMessage)
    {
        Data = data;
        Success = success;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }

    public T? Data { get; }
    public bool Success { get; }
    public ServiceErrorType? ErrorType { get; }
    public string? ErrorMessage { get; }

    public static ServiceResult<T> Ok(T data) => new(data, true, null, null);

    public static ServiceResult<T> Fail(ServiceErrorType errorType, string errorMessage) =>
        new(default, false, errorType, errorMessage);
}
