namespace HiddenWing.BrandCenter.Application.Exceptions;

public sealed class ConflictException : AppException
{
    public ConflictException(string message, string errorCode)
        : base(message, errorCode, StatusCodes.Conflict)
    {
    }
}
