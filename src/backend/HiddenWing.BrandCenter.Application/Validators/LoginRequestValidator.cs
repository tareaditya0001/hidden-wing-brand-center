using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Auth;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Username).NotEmpty().MaximumLength(80);
        RuleFor(request => request.Password).NotEmpty().MaximumLength(200);
    }
}
