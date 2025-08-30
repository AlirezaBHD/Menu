using Application.Dto.Category;
using Application.Dto.Shared;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
    Task<CategoryResponse> GetCategoryByIdAsync(int categoryId);
    Task DeleteCategoryAsync(int id);
    Task UpdateCategoryAsync(int id, UpdateCategoryRequest dto);
    Task<IEnumerable<CategoryListResponse>> GetCategoryListAsync();
    Task UpdateCategoryOrderAsync(List<OrderDto> dto);
}