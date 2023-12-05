using Microsoft.AspNetCore.Identity;

namespace Ahu.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductReview> ProductReviews { get; set; }
    public bool IsDeleted { get; set; }
}