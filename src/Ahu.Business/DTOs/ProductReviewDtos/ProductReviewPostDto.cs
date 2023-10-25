namespace Ahu.Business.DTOs.ProductReviewDtos;

public class ProductReviewPostDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public int Raiting { get; set; }
}