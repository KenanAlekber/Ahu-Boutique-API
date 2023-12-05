using Ahu.Business.DTOs.UserDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Fullname).NotEmpty().MaximumLength(25).MinimumLength(6);
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(20);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}