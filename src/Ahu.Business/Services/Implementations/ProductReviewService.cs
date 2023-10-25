using Ahu.Business.DTOs.ProductReviewDtos;
using Ahu.Business.Exceptions.ProductExceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class ProductReviewService : IProductReviewService
{
    private readonly IProductReviewRepository _productReviewRepository;
    private readonly AppDbContext _context;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductReviewService(IProductReviewRepository productReviewRepository,AppDbContext context, IMapper mapper, IProductRepository productRepository)
    {
        _productReviewRepository = productReviewRepository;
        _context = context;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<List<ProductReviewGetDto>> GetAllProductReviewsAsync()
    {
        List<ProductReview> productReviews = await _productReviewRepository.GetFiltered(pr => true).ToListAsync();
        List<ProductReviewGetDto> productReviewGetDtos = null;

        return _mapper.Map<List<ProductReviewGetDto>>(productReviews);
    }

    public async Task<ProductReviewGetDto> GetProductReviewAsync(Guid productId)
    {
        List<ProductReview> productReviews = await _productReviewRepository.GetFiltered(pr => pr.ProductId == productId).ToListAsync();
        List<ProductReviewGetDto> productReviewGetDtos = null;

        if (productReviews is null)
            throw new ProductNotFoundException($"Product is not found by id: {productId}");

        return _mapper.Map<ProductReviewGetDto>(productReviews);
    }

    public async Task<Guid> CreateProductReviewAsync(ProductReviewPostDto productReviewPostDto)
    {
        //ProductReview productReview = _mapper.Map<ProductReview>(productReviewPostDto);

        ProductReview productReview = new ProductReview
        {
            Description = productReviewPostDto.Description,
            Date = DateTime.UtcNow.ToString(),
            //UserId = productReviewPostDto.UserId,
            ProductId = productReviewPostDto.ProductId,
        };

        //productReview.Date = DateTime.UtcNow.ToString();

        //Product product = await _productRepository.GetSingleAsync(p => p.Id == productReviewPostDto.ProductId);
        Product product = await _productRepository.GetFiltered(x=>true).FirstOrDefaultAsync(p => p.Id == productReviewPostDto.ProductId);


        if (product is null)
            throw new ProductNotFoundException($"Product is not found by id: {productReviewPostDto.ProductId}");

        product.Rate = (product.Rate + productReviewPostDto.Raiting) / 2;

        //await _productReviewRepository.CreateAsync(productReview);
        //var result = await _productReviewRepository.SaveAsync();
        //_context.ProductReviews.Add(productReview);
        _context.ProductReviews.Add(productReview);
        _context.SaveChanges();

        _productRepository.Update(product);
        await _productRepository.SaveAsync();

        return productReview.Id;
    }
}