namespace Ahu.Business.DTOs.UserDtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public bool EmailConfirm { get; set; }
}