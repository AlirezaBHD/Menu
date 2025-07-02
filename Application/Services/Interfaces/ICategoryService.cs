using Application.Dto.Category;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> GetCategoryById(Guid categoryId);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest dto);
}