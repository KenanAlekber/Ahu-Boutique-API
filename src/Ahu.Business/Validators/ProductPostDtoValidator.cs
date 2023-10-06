﻿using Ahu.Business.DTOs.ProductDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
{
    public ProductPostDtoValidator()
    {
        {
            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(250);
            RuleFor(a => a.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(2500);
            RuleFor(a => a.Color)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(a => a.Size)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);
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
            //RuleFor(c => c).Custom((c, context) =>
            //{
            //    if (c.Price != null)
            //    {
            //        if (c.Price < 0)
            //        {
            //            context.AddFailure(nameof(c.Price), "");
            //        }
            //    }
            //})
            //    .NotNull()
            //    .NotEmpty();
        }
    }
}
