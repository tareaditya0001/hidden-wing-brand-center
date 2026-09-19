namespace HiddenWing.BrandCenter.Application.Exceptions;

public sealed class UnauthorizedAppException : AppException
{
    public UnauthorizedAppException(string message, string errorCode)
        : base(message, errorCode, StatusCodes.Unauthorized)
    {
    }
}
