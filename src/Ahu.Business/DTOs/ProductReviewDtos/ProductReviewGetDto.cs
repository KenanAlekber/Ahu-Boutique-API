namespace Ahu.Business.DTOs.ProductReviewDtos;

public class ProductReviewGetDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public int Raiting { get; set;}
    public string Answer { get; set; }
    public string Date { get; set; }
}

public class UserInProductReviewGetDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class ProductInProductReviewGetDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}