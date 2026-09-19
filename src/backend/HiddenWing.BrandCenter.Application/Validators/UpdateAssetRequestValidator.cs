using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Asset;

namespace HiddenWing.BrandCenter.Application.Validators;

public sealed class UpdateAssetRequestValidator : AbstractValidator<UpdateAssetRequest>
{
    public UpdateAssetRequestValidator()
    {
        RuleFor(request => request.BrandId).NotEmpty();
        RuleFor(request => request.Name).NotEmpty().MaximumLength(160);
        RuleFor(request => request.Url).NotEmpty().MaximumLength(1000);
        RuleFor(request => request.FileSize).GreaterThanOrEqualTo(0);
    }
}
