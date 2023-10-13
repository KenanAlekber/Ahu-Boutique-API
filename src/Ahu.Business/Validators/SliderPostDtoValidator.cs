using Ahu.Business.DTOs.SliderDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class SliderPostDtoValidator : AbstractValidator<SliderPostDto>
{
    public SliderPostDtoValidator()
    {
        RuleFor(s => s.Title).NotEmpty().MaximumLength(20).MinimumLength(2);
        RuleFor(s => s.ImageFile).NotNull();
        RuleFor(s => s).Custom((s, context) =>
        {
            if (s.ImageFile.Length > 2097152)
                context.AddFailure(nameof(s.ImageFile), "ImageFile must be less or equal than 2MB");

            if (s.ImageFile.ContentType != "image/jpeg" && s.ImageFile.ContentType != "image/png" && s.ImageFile.ContentType != "image/webp")
                context.AddFailure(nameof(s.ImageFile), "ImageFile must be image/jpeg or image/webp");
        });
    }
}