using Ahu.Business.DTOs.UserDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(p => p.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        RuleFor(p => p.NewPassword).NotEmpty().MinimumLength(6).MaximumLength(20);
        RuleFor(p => p.ConfirmPassword).Equal(x => x.NewPassword).WithMessage("Passwords do not match");
    }
}