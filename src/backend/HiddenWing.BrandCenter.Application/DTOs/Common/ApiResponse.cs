namespace HiddenWing.BrandCenter.Application.DTOs.Common;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }

    public T? Data { get; init; }

    public string? Message { get; init; }

    public string? ErrorCode { get; init; }

    public IReadOnlyList<string>? Errors { get; init; }

    public PaginationDto? Pagination { get; init; }

    public static ApiResponse<T> Ok(T data, PaginationDto? pagination = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Pagination = pagination
        };
    }

    public static ApiResponse<T> Fail(string message, string? errorCode = null, IReadOnlyList<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors
        };
    }
}

public sealed class ApiResponse
{
    public bool Success { get; init; }

    public string? Message { get; init; }

    public string? ErrorCode { get; init; }

    public IReadOnlyList<string>? Errors { get; init; }

    public static ApiResponse Fail(string message, string? errorCode = null, IReadOnlyList<string>? errors = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors
        };
    }
}
