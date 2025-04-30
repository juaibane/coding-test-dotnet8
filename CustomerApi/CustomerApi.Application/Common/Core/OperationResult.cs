namespace CustomerApi.Application.Common.Core;

public class OperationResult
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new();
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public static OperationResult Ok(string? message = null)
        => new OperationResult { Success = true, Message = message, StatusCode = 200 };

    public static OperationResult Created(string? message = null)
        => new OperationResult { Success = true, Message = message, StatusCode = 201 };

    public static OperationResult BadRequest(List<string> errors)
        => new OperationResult { Success = false, Errors = errors, StatusCode = 400 };

    public static OperationResult BadRequest(string error)
        => new OperationResult { Success = false, Errors = new List<string> { error }, StatusCode = 400 };

    public static OperationResult NotFound(string error)
        => new OperationResult { Success = false, Errors = new List<string> { error }, StatusCode = 404 };

    public static OperationResult InternalError(string error)
        => new OperationResult { Success = false, Errors = new List<string> { error }, StatusCode = 500 };
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; set; }

    public static OperationResult<T> Ok(T data, string? message = null)
        => new OperationResult<T> { Success = true, Data = data, Message = message, StatusCode = 200 };

    public static OperationResult<T> Created(T data, string? message = null)
        => new OperationResult<T> { Success = true, Data = data, Message = message, StatusCode = 201 };
}
