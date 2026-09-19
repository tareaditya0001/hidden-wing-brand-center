using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.Validators;

namespace HiddenWing.BrandCenter.UnitTests.Validators;

public sealed class CreateBrandRequestValidatorTests
{
    private readonly CreateBrandRequestValidator _validator = new();

    [Fact]
    public void Validate_AcceptsCanonicalBrand()
    {
        var result = _validator.Validate(new CreateBrandRequest
        {
            Name = "Hidden Wing",
            Slug = "hidden-wing",
            CompanyName = "Hidden Wing"
        });

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("Hidden Wing")]
    [InlineData("HIDDEN-WING")]
    [InlineData("hidden_wing")]
    public void Validate_RejectsInvalidSlug(string slug)
    {
        var result = _validator.Validate(new CreateBrandRequest
        {
            Name = "Hidden Wing",
            Slug = slug,
            CompanyName = "Hidden Wing"
        });

        result.IsValid.Should().BeFalse();
    }
}
