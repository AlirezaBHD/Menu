using Application.Dto.Category;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> CreateCategoryAsync(Guid restaurantId, CreateCategoryRequest createCategoryRequest);
    Task<CategoryResponse> GetCategoryByIdAsync(Guid categoryId);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest dto);
}