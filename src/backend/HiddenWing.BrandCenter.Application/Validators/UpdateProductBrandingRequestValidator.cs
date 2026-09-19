using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Product;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class UpdateProductBrandingRequestValidator : AbstractValidator<UpdateProductBrandingRequest>
{
    public UpdateProductBrandingRequestValidator()
    {
        RuleFor(request => request.DisplayName).MaximumLength(160);
        RuleFor(request => request.ShortName).MaximumLength(80);
        RuleFor(request => request.Tagline).MaximumLength(240);
        RuleFor(request => request.FontFamily).MaximumLength(160);
        RuleFor(request => request.SupportEmail).EmailAddress().When(request => !string.IsNullOrWhiteSpace(request.SupportEmail));
        RuleFor(request => request.PrimaryColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.SecondaryColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.AccentColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.BackgroundColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.SurfaceColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.TextColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.MutedTextColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.BorderColor).Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
    }
}
