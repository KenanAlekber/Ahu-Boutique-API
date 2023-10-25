namespace Ahu.Business.DTOs.ProductDtos;

public class ProductGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public int StockCount { get; set; }
    public int Rate { get; set; }
    public BrandInProductGetDto Brand { get; set; }
    public CategoryInProductGetDto Category { get; set; }
    public List<ProductImagesInProductGetDto> ProductImages { get; set; }
    public ProductImagesInProductGetDto PosterImage { get; set; }
}

public class BrandInProductGetDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class CategoryInProductGetDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class ProductImagesInProductGetDto
{
    public Guid ProductId { get; set; }
    public string ImageName { get; set; }
    public string ImageUrl { get; set; }
    public bool PosterStatus { get; set; }
}