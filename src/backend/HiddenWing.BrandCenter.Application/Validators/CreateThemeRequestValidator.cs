using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Theme;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class CreateThemeRequestValidator : AbstractValidator<CreateThemeRequest>
{
    public CreateThemeRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(120);
        RuleFor(request => request.Slug).NotEmpty().MaximumLength(80).Matches(SlugRules.Pattern).WithMessage(SlugRules.Message);
        RuleFor(request => request.PrimaryColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.SecondaryColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.AccentColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.BackgroundColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.SurfaceColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.TextColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.MutedTextColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.BorderColor).NotEmpty().Must(ColorRules.IsValid).WithMessage(ColorRules.Message);
        RuleFor(request => request.FontFamily).NotEmpty().MaximumLength(160);
        RuleFor(request => request.BorderRadius).NotEmpty().MaximumLength(32);
    }
}
