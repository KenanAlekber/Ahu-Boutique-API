using Ahu.Business.DTOs.ProductDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Exceptions.ProductExceptions;
using Ahu.Business.Helpers;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Implementations;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IProductImageRepository _productImageRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper, IProductImageRepository productImageRepository, IBrandRepository brandRepository, ICategoryRepository categorieRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productImageRepository = productImageRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categorieRepository;
    }

    public async Task<List<ProductGetDto>> GetAllProductsAsync()
    {
        List<Product> products = await _productRepository.GetFiltered(p => true, "Brand", "Category").ToListAsync();
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
        Product product = await _productRepository.GetSingleAsync(c => c.Id == id, "Brand", "Category");

        if (product is null)
            throw new ProductNotFoundException($"Product is not found by id: {id}");

        ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);

        List<ProductImage> productImages = await _productImageRepository.GetFiltered(x => x.ProductId == productDto.Id).ToListAsync();

        foreach (var item in productImages)
        {
            if (item.PosterStatus == true)
                productDto.PosterImage = _mapper.Map<ProductImagesInProductGetDto>(item);

            productDto.ProductImages.Add(_mapper.Map<ProductImagesInProductGetDto>(item));
        }

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

    public void Edit(ProductPutDto productPutDto)
    {
        // "ProductImages", "Brand", "Category"
        Product product = _productRepository.GetAll(x => true).FirstOrDefault(x => x.Id == productPutDto.Id);

        List<RestExceptionError> errors = new List<RestExceptionError>();

        //if (!_categoryRepository.IsExistAsync(x => x.Id == productPutDto.CategoryId))
        //    errors.Add(new RestExceptionError("CategoryId", "CategoryId is not correct"));

        //if (!_brandRepository.IsExistAsync(x => x.Id == productPutDto.BrandId))
        //    errors.Add(new RestExceptionError("BrandId", "BrandId is not correct"));

        if (errors.Count > 0) throw new RestException(System.Net.HttpStatusCode.BadRequest, errors);

        string oldPoster = null;
        string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

        if (productPutDto.PosterImageFile != null)
        {
            var postImg = product.ProductImages.FirstOrDefault(x => x.PosterStatus == true);
            oldPoster = postImg.ImageName;
            postImg.ImageName = FileManager.Save(productPutDto.PosterImageFile, rootPath, "uploads/products");
            postImg.ImageUrl = "/uploads/products/" + postImg.ImageName;
        }

        var removedImages = product.ProductImages.FindAll(x => x.PosterStatus == false);
        if (productPutDto.ImageFiles != null)
        {
            product.ProductImages.RemoveAll(x => x.PosterStatus == false);
        }

        if (productPutDto.ImageFiles != null)
        {
            foreach (var img in productPutDto.ImageFiles)
            {
                var imageName = FileManager.Save(img, rootPath, "uploads/products");
                ProductImage productImage = new ProductImage()
                {
                    ImageName = imageName,
                    ImageUrl = "/uploads/products/" + imageName
                };
                product.ProductImages.Add(productImage);
            }
        }

        product.CostPrice = productPutDto.CostPrice;
        product.SalePrice = productPutDto.SalePrice;
        product.DiscountPercent = productPutDto.DiscountPercent;
        product.BrandId = productPutDto.BrandId;
        product.CategoryId = productPutDto.CategoryId;
        product.Name = productPutDto.Name;
        product.StockCount = productPutDto.StockCount;
        product.Rate = productPutDto.Rate;
        product.Description = productPutDto.Description;

        _productRepository.SaveAsync();

        if (oldPoster != null) FileManager.Delete(rootPath, "uploads/products", oldPoster);

        if (productPutDto.ImageFiles != null)
        {
            foreach (var img in removedImages)
            {
                FileManager.Delete(rootPath, "uploads/products", img.ImageName);
            }
        }
    }

    public void DeleteProduct(Guid id)
    {
        Product product = _productRepository.GetAll(x => true).FirstOrDefault(x => x.Id == id);

        if (product is null)
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Product not found");

        _productRepository.Delete(product);
        _productRepository.SaveAsync();
    }
}