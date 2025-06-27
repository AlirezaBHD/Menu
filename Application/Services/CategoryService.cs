using Application.Dto.Category;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    #region Injection

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        : base(mapper, categoryRepository)
    {
    }

    #endregion

    public async Task<CategoryResponse> CreateCategory(Guid restaurantId, CreateCategoryRequest createCategoryRequest)
    {
        var entity = Mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        entity.RestaurantId = restaurantId;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Category, CategoryResponse>(entity);
        return response;
    }
}