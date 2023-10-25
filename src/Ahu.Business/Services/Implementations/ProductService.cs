using Ahu.Business.DTOs.ProductDtos;
using Ahu.Business.Exceptions.ProductExceptions;
using Ahu.Business.Helpers;
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
    private readonly IProductImageRepository _productImageRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper, IProductImageRepository productImageRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productImageRepository = productImageRepository;
    }

    public async Task<List<ProductGetDto>> GetAllProductsAsync()
    {
        List<Product> products = await _productRepository.GetFiltered(p => true,"Brand","Category").ToListAsync();
        List<ProductGetDto> productDtos = null;

        try
        {
            productDtos = _mapper.Map<List<ProductGetDto>>(products);

            foreach (var productDto in productDtos)
            {
                List<ProductImage> productImages = await _productImageRepository.GetFiltered(x => x.ProductId == productDto.Id).ToListAsync();

                foreach (var imageDto in productImages)
                {
                    if (imageDto.PosterStatus == true)
                        productDto.PosterImage = _mapper.Map<ProductImagesInProductGetDto>(imageDto);
                    else
                        productDto.ProductImages.Add(_mapper.Map<ProductImagesInProductGetDto>(imageDto));
                }
            }
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
        var product = await _productRepository.GetSingleAsync(c => c.Id == id, "Product");

        if (product == null)
            throw new ProductNotFoundException($"Product is not found by id: {id}");

        var productDto = _mapper.Map<ProductGetDto>(product);

        return productDto;
    }

    public async Task<Guid> CreateProductAsync(ProductPostDto productPostDto)
    {
        Product product = _mapper.Map<Product>(productPostDto);

        await _productRepository.CreateAsync(product);
        await _productRepository.SaveAsync();

        string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

        for (int idx = 0; idx < productPostDto.ImageFiles.Count(); idx++)
        {
            ProductImage img = new ProductImage();

            img.ProductId = product.Id;
            img.ImageName = FileManager.Save(productPostDto.ImageFiles[idx], rootPath, "uploads/products");
            img.ImageUrl = "/uploads/products/" + img.ImageName;
            img.PosterStatus = false;

            await _productImageRepository.CreateAsync(img);
            await _productImageRepository.SaveAsync();
        }

        ProductImage posterImg = new ProductImage();
        posterImg.ProductId = product.Id;
        posterImg.ImageName = FileManager.Save(productPostDto.PosterImageFile, rootPath, "uploads/products");
        posterImg.ImageUrl = "/uploads/products/" + posterImg.ImageName;
        posterImg.PosterStatus = true;

        await _productImageRepository.CreateAsync(posterImg);
        await _productImageRepository.SaveAsync();

        return product.Id;
    }
}