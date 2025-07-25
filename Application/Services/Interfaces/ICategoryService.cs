using Application.Dto.Category;
using Application.Dto.Shared;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
    Task<CategoryResponse> GetCategoryByIdAsync(Guid categoryId);
    Task DeleteCategoryAsync(Guid id);
    Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest dto);
    Task<IEnumerable<CategoryListResponse>> GetCategoryListAsync();
    Task UpdateCategoryOrderAsync(List<OrderDto> dto);
}