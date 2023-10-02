using Microsoft.AspNetCore.Identity;

namespace Ahu.Core.Entities.Identity;

public class User : IdentityUser
{
    public string Fullname { get; set; }
    public bool IsDeleted { get; set; }
}