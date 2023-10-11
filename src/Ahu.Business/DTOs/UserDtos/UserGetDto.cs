namespace Ahu.Business.DTOs.UserDtos;

public record UserGetDto(Guid Id, string Fullname, string UserName, string Email, string Address, string Phone, bool IsAdmin, bool EmailConfirm);