namespace Ahu.Business.DTOs.UserDtos;

public record ChangePasswordDto(string Password, string NewPassword, string ConfirmPassword);