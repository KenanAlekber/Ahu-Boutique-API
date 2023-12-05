namespace Ahu.Business.DTOs.UserDtos;

public class UserPutDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string Password { get; set; }
}