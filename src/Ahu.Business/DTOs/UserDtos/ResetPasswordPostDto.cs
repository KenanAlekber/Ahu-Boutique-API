namespace Ahu.Business.DTOs.UserDtos;

public record ResetPasswordPostDto(string Email, string Token, string Password, string ConfirmPassword);