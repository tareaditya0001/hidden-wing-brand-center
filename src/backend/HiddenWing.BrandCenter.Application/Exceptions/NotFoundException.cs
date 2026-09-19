namespace HiddenWing.BrandCenter.Application.Exceptions;

public sealed class NotFoundException : AppException
{
    public NotFoundException(string message, string errorCode)
        : base(message, errorCode, StatusCodes.NotFound)
    {
    }
}
