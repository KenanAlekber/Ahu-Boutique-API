using Ahu.Business.DTOs.UserDtos;
using FluentValidation;

namespace Ahu.Business.Validators;

public class UserPutDtoValidator : AbstractValidator<UserPutDto>
{
    public UserPutDtoValidator()
    {
        RuleFor(x => x.FullName).MaximumLength(25);
        RuleFor(x => x.UserName).MaximumLength(20);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}