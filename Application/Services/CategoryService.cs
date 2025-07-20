using System.Linq.Expressions;
using Application.Dto.Category;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    #region Injection

    private readonly ISectionRepository _sectionRepository;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ISectionRepository sectionRepository, ILogger<Category> logger)
        : base(mapper, categoryRepository, logger)
    {
        _sectionRepository = sectionRepository;
    }

    #endregion

    #region Availability Expression

    public static Expression<Func<Category, bool>> IsAvailable(TimeSpan nowTime)
    {
        return c =>
            c.AvailabilityPeriod.IsActive && (
                c.AvailabilityPeriod.ActivityType == ActivityEnum.Unlimited ||
                (
                    (c.AvailabilityPeriod.ActivityType == ActivityEnum.ActivePeriod &&
                     nowTime >= c.AvailabilityPeriod.FromTime &&
                     nowTime <= c.AvailabilityPeriod.ToTime)
                    ||
                    (c.AvailabilityPeriod.ActivityType == ActivityEnum.InactivePeriod &&
                     (nowTime < c.AvailabilityPeriod.FromTime ||
                      nowTime > c.AvailabilityPeriod.ToTime))
                ));
    }

    #endregion
    
    public async Task<CategoryResponse> CreateCategoryAsync(Guid restaurantId, CreateCategoryRequest createCategoryRequest)
    {
        var entity = Mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        entity.RestaurantId = restaurantId;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Category, CategoryResponse>(entity);
        Logger.LogInformation("Created new category: {@Category}", response);
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
        Logger.LogInformation("Deleting category: {@Category}", category);
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest dto)
    {
        var category = await Repository.GetQueryable().Include(c => c.Sections)
            .FirstAsync(c => c.Id == id);
        
        category = Mapper.Map(dto, category);
        
        var sectionIds = dto.SectionIds?.Distinct().ToList() ?? [];

        var sections = await _sectionRepository.GetQueryable()
            .Where(s => sectionIds.Contains(s.Id))
            .ToListAsync();
        
        category.Sections = sections;

        Repository.Update(category);
        await Repository.SaveAsync();
        Logger.LogInformation("Updated category with ID: {Id}. Data: {@UpdateData}", id, category);
    }
}