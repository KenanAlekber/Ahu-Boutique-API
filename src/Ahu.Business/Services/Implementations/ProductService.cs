using Ahu.Business.DTOs.ProductDtos;
using Ahu.Business.Exceptions.ProductExceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductGetDto>> GetAllProductsAsync(string? search)
    {
        var products = await _productRepository.GetFiltered(p => search != null ? p.Name.Contains(search) : true).ToListAsync();

        List<ProductGetDto> productDtos = null;

        try
        {
            productDtos = _mapper.Map<List<ProductGetDto>>(products);
        }
        catch (ProductNotFoundException ex)
        {
            throw new ProductNotFoundException(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return productDtos;
    }

    public async Task<ProductGetDto> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetSingleAsync(c => c.Id == id);

        if (product == null)
            throw new ProductNotFoundException($"Product is not found by id: {id}");

        var productDto = _mapper.Map<ProductGetDto>(product);

        return productDto;
    }

    public async Task CreateProductAsync(ProductPostDto productPostDto)
    {
        Product product = _mapper.Map<Product>(productPostDto);

        await _productRepository.CreateAsync(product);
        await _productRepository.SaveAsync();
    }
}