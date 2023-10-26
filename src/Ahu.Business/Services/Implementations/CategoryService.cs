using Ahu.Business.DTOs.CategoryDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Exceptions.BrandExceptions;
using Ahu.Business.Exceptions.CategoryExceptions;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities;
using Ahu.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ahu.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryGetDto>> GetAllCategorysAsync()
    {
        var categories = await _categoryRepository.GetAll(c => true).ToListAsync();

        List<CategoryGetDto> categoryDtos = null;

        try
        {
            categoryDtos = _mapper.Map<List<CategoryGetDto>>(categories);
        }
        catch (BrandNotFoundException ex)
        {
            throw new BrandNotFoundException(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return categoryDtos;
    }

    public async Task<CategoryGetDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetSingleAsync(c => c.Id == id);

        if (category == null)
            throw new CategoryNotFoundException($"Category is not found by id: {id}");

        var categoryDto = _mapper.Map<CategoryGetDto>(category);

        return categoryDto;
    }

    public async Task<Guid> CreateCategoryAsync(CategoryPostDto categoryPostDto)
    {
        Category category = _mapper.Map<Category>(categoryPostDto);

        await _categoryRepository.CreateAsync(category);
        await _categoryRepository.SaveAsync();
        return category.Id;
    }

    public void DeleteCategory(Guid id)
    {
        Category category = _categoryRepository.GetAll(x => true).FirstOrDefault(x => x.Id == id);

        if (category is null) 
            throw new RestException(System.Net.HttpStatusCode.NotFound, "Category not found");

        _categoryRepository.Delete(category);
        _categoryRepository.SaveAsync();
    }
}