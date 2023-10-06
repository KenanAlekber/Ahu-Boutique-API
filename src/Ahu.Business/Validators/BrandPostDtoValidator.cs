using Ahu.Business.DTOs.BrandDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class BrandPostDtoValidator : AbstractValidator<BrandPostDto>
{
    public BrandPostDtoValidator()
    {
        RuleFor(b => b.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(20);
    }
}