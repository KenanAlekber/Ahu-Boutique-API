using Ahu.Business.DTOs.CategoryDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
{
    public CategoryPostDtoValidator()
    {
        RuleFor(b => b.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(20);
    }
}