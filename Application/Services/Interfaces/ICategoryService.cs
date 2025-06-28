using Application.Dto.Category;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> CreateCategory(Guid restaurantId, CreateCategoryRequest createCategoryRequest);
    Task<CategoryResponse> GetCategoryById(Guid categoryId);
}