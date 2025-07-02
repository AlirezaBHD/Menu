using Application.Dto.Category;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    #region Injection

    private readonly ISectionRepository _sectionRepository;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ISectionRepository sectionRepository)
        : base(mapper, categoryRepository)
    {
        _sectionRepository = sectionRepository;
    }

    #endregion

    public async Task<CategoryResponse> CreateCategoryAsync(Guid restaurantId, CreateCategoryRequest createCategoryRequest)
    {
        var entity = Mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        entity.RestaurantId = restaurantId;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Category, CategoryResponse>(entity);
        return response;
    }

    public async Task<CategoryResponse> GetCategoryByIdAsync(Guid categoryId)
    {
        var response =
            await GetByIdProjectedAsync<CategoryResponse>(categoryId, trackingBehavior: TrackingBehavior.AsNoTracking);
        return response;
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await Repository.GetByIdAsync(id);
        Repository.Remove(category);
        await Repository.SaveAsync();
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest dto)
    {
        var category = await Repository.GetByIdAsync(id);
        category = Mapper.Map(dto, category);
        
        var sectionIds = dto.SectionIds?.Distinct().ToList() ?? [];

        var sections = await _sectionRepository.GetQueryable()
            .Where(s => sectionIds.Contains(s.Id))
            .ToListAsync();
        
        category.Sections = sections;

        Repository.Update(category);
        await Repository.SaveAsync();
    }
}