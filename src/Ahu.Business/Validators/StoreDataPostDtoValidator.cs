using Ahu.Business.DTOs.StoreDataDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class StoreDataPostDtoValidator : AbstractValidator<StoreDataPostDto>
{
    public StoreDataPostDtoValidator()
    {
        RuleFor(sd => sd).Custom((sd, context) =>
        {
            if (sd.LogoImageFile.Length > 2097152)
                context.AddFailure(nameof(sd.LogoImageFile), "ImageFile must be less or equal than 2MB");

            if (sd.LogoImageFile.ContentType != "image/jpeg" && sd.LogoImageFile.ContentType != "image/png" && sd.LogoImageFile.ContentType != "image/webp")
                context.AddFailure(nameof(sd.LogoImageFile), "ImageFile must be image/jpeg, image/png or image/webp");
        });

        RuleFor(sd => sd).Custom((sd, context) =>
        {
            if (sd.EmptyBasketImageFile.Length > 2097152)
                context.AddFailure(nameof(sd.EmptyBasketImageFile), "ImageFile must be less or equal than 2MB");

            if (sd.EmptyBasketImageFile.ContentType != "image/jpeg" && sd.EmptyBasketImageFile.ContentType != "image/png" 
            && sd.LogoImageFile.ContentType != "image/webp")
                context.AddFailure(nameof(sd.EmptyBasketImageFile), "ImageFile must be image/jpeg, image/png or image/webp");
        });
    }
}