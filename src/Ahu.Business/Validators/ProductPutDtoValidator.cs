using Ahu.Business.DTOs.ProductDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class ProductPutDtoValidator : AbstractValidator<ProductPutDto>
{
    public ProductPutDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(25).MinimumLength(2);
        RuleFor(p => p.SalePrice).GreaterThanOrEqualTo(pr => pr.CostPrice);
        RuleFor(p => p.CostPrice).GreaterThanOrEqualTo(0);
        RuleFor(p => p.DiscountPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

        RuleFor(p => p).Custom((pr, context) =>
        {
            if (pr.DiscountPercent > 0)
            {
                var price = pr.SalePrice * (100 - pr.DiscountPercent) / 100;
                if (pr.CostPrice > price)
                {
                    context.AddFailure(nameof(pr.DiscountPercent), "DiscountPercent is incorrect");
                }
            }
        });

        RuleFor(p => p).Custom((pr, context) =>
        {
            if (pr.PosterImageFile != null)
            {
                if (pr.PosterImageFile.Length > 6291456)
                    context.AddFailure(nameof(pr.PosterImageFile), "ImageFile must be less or equal than 6MB");

                if (pr.PosterImageFile.ContentType != "image/jpeg" && pr.PosterImageFile.ContentType != "image/png"
                && pr.PosterImageFile.ContentType != "image/webp")
                    context.AddFailure(nameof(pr.PosterImageFile), "ImageFile must be image/jpeg, image/png or image/webp");
            }

            if (pr.ImageFiles != null)
            {
                foreach (var img in pr.ImageFiles)
                {
                    if (img.Length > 6291456)
                        context.AddFailure(nameof(img), "ImageFile must be less or equal than 6MB");

                    if (img.ContentType != "image/jpeg" && img.ContentType != "image/png"
                    && pr.PosterImageFile.ContentType != "image/webp")
                        context.AddFailure(nameof(img), "ImageFile must be image/jpeg, image/png or image/webp");
                }
            }
        });
    }
}