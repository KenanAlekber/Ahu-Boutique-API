namespace Ahu.Business.DTOs.UserDtos;

public record UserRegisterDto(string Fullname, string Username, string Email, string Password, string ConfirmPassword);