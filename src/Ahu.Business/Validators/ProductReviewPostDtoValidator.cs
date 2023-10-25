using Ahu.Core.Entities;
using FluentValidation;

namespace Ahu.Business.Validators;

public class ProductReviewPostDtoValidator : AbstractValidator<ProductReview>
{
    public ProductReviewPostDtoValidator()
    {
        RuleFor(pr => pr.UserId).NotNull();
        RuleFor(pr => pr.ProductId).NotNull();
        RuleFor(pr => pr.Description).NotNull().NotEmpty().MinimumLength(2).MaximumLength(2500);
    }
}