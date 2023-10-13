using Ahu.Business.DTOs.ProductDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
{
    public ProductPostDtoValidator()
    {
        {
            RuleFor(a => a.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(a => a.Description).NotNull().NotEmpty().MinimumLength(2).MaximumLength(2500);
            RuleFor(a => a.Color).NotNull().NotEmpty().MinimumLength(2).MaximumLength(15);
            RuleFor(a => a.Size).NotNull().NotEmpty().MinimumLength(1).MaximumLength(10);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.DiscountPercent > 0)
                {
                    var price = x.SalePrice * (100 - x.DiscountPercent) / 100;
                    if (x.CostPrice > price)
                    {
                        context.AddFailure(nameof(x.DiscountPercent), "DiscountPercent is incorrect");
                    }
                }
            });

            RuleFor(s => s).Custom((s, context) =>
            {
                if (s != null && s.PosterImageFile != null)
                {
                    if (s.PosterImageFile.Length > 2097152)
                        context.AddFailure(nameof(s.PosterImageFile), "ImageFile must be less or equal than 2MB");

                    if (s.PosterImageFile.ContentType != "image/jpeg" && s.PosterImageFile.ContentType != "image/png" 
                    && s.PosterImageFile.ContentType != "image/webp")
                        context.AddFailure(nameof(s.PosterImageFile), "ImageFile must be image/jpeg, image/png or image/webp");
                }

                if (s != null && s.ImageFiles != null)
                {
                    foreach (var image in s.ImageFiles)
                    {
                        if (image.Length > 2097152)
                            context.AddFailure(nameof(image), "ImageFile must be less or equal than 2MB");

                        if (image.ContentType != "image/jpeg" && image.ContentType != "image/png"
                        && s.PosterImageFile.ContentType != "image/webp")
                            context.AddFailure(nameof(image), "ImageFile must be image/jpeg, image/png or image/webp");
                    }
                }
            });
        }
    }
}