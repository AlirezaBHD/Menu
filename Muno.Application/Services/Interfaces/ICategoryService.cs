using Muno.Domain.Entities.Categories;
using Muno.Application.Dto.Category;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
    Task<CategoryResponse> GetCategoryByIdAsync(int categoryId);
    Task DeleteCategoryAsync(int id);
    Task UpdateCategoryAsync(int id, UpdateCategoryRequest dto);
    Task<IEnumerable<CategoryListResponse>> GetCategoryListAsync();
    Task UpdateCategoryOrderAsync(List<OrderDto> dto);
}