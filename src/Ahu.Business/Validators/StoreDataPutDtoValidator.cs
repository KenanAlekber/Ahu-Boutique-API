using Ahu.Business.DTOs.StoreDataDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class StoreDataPutDtoValidator : AbstractValidator<StoreDataPutDto>
{
    public StoreDataPutDtoValidator()
    {
        When(x => x.LogoImageFile != null, () =>
        {
            RuleFor(x => x.LogoImageFile.Length)
                .LessThanOrEqualTo(6291456)
                .WithMessage("LogoImageFile must be less or equal than 6MB");

            RuleFor(x => x.LogoImageFile.ContentType)
                .Must(contentType => contentType == "image/jpeg" || contentType == "image/png" || contentType == "image/webp")
                .WithMessage("LogoImageFile must be image/jpeg, image/png or image/webp");
        });
    }
}