using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Product;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(request => request.BrandId).NotEmpty();
        RuleFor(request => request.Name).NotEmpty().MaximumLength(120);
        RuleFor(request => request.Slug).NotEmpty().MaximumLength(80).Matches(SlugRules.Pattern).WithMessage(SlugRules.Message);
        RuleFor(request => request.Description).MaximumLength(500);
        RuleFor(request => request.ApplicationUrl).MaximumLength(500);
    }
}
