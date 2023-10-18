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
        RuleFor(x => x.Address).MaximumLength(100);
        RuleFor(x => x.Phone).Matches(@"^(\+\d{1,3})?\s?\d{11,13}$").WithMessage("Please enter a valid phone number");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}