using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Brand;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(120);
        RuleFor(request => request.CompanyName).NotEmpty().MaximumLength(160);
        RuleFor(request => request.Slug).NotEmpty().MaximumLength(80).Matches(SlugRules.Pattern).WithMessage(SlugRules.Message);
        RuleFor(request => request.Description).MaximumLength(500);
    }
}
