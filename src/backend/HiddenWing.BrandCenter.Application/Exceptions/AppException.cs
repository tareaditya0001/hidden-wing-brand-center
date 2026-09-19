namespace HiddenWing.BrandCenter.Application.Exceptions;

/// <summary>
/// Base application exception mapped to a consistent API error payload.
/// </summary>
public abstract class AppException : Exception
{
    protected AppException(string message, string errorCode, int statusCode)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    public string ErrorCode { get; }

    public int StatusCode { get; }
}
