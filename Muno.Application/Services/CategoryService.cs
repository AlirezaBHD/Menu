using System.Linq.Expressions;
using AutoMapper;
using Muno.Domain.Entities;
using Muno.Domain.Entities.Categories;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.Category;
using Muno.Application.Dto.Shared;
using Muno.Application.Exceptions;
using Muno.Application.Localization;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    ISectionRepository sectionRepository,
    ILogger<Category> logger,
    ICurrentUser user)
    : Service<Category>(mapper, categoryRepository, logger), ICategoryService
{
    public static Expression<Func<Category, bool>> IsAvailable(TimeSpan nowTime)
    {
        return c =>
            c.ActivityPeriod.IsActive && (
                c.ActivityPeriod.ActivityType == ActivityEnum.Unlimited ||
                (
                    (c.ActivityPeriod.ActivityType == ActivityEnum.ActivePeriod &&
                     nowTime >= c.ActivityPeriod.FromTime &&
                     nowTime <= c.ActivityPeriod.ToTime)
                    ||
                    (c.ActivityPeriod.ActivityType == ActivityEnum.InactivePeriod &&
                     (nowTime < c.ActivityPeriod.FromTime ||
                      nowTime > c.ActivityPeriod.ToTime))
                ));
    }
    
    
    public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest)
    {
        
        var entity = Mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        
        entity.RestaurantId = user.RestaurantId;
        
        var count = Queryable.Count();
        entity.Order = count + 1;
        
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Category, CategoryResponse>(entity);
        Logger.LogInformation("Created new category: {@Category}", response);
        return response;
    }


    public async Task<CategoryResponse> GetCategoryByIdAsync(int categoryId)
    {
        
        var query = Queryable.Include(c => c.Translations)
            .Include(c => c.Sections).ThenInclude(s => s.Translations);
        
        var response =
            await GetByIdProjectedAsync<CategoryResponse>(categoryId,
                query: query,
                trackingBehavior: TrackingBehavior.AsNoTracking);


        return response;
    }


    public async Task DeleteCategoryAsync(int id)
    {
        var category = await Repository.GetByIdAsync(id);
        Repository.Remove(category);
        await Repository.SaveAsync();
        Logger.LogInformation("Deleting category: {@Category}", category);
    }


    public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest dto)
    {
        var category = await Queryable.Include(c => c.Sections)
            .Include(c => c.Translations)
            .FirstAsync(c => c.Id == id);
        
        category = Mapper.Map(dto, category);
        
        var sectionIds = dto.SectionIds.Distinct().ToList();

        var sections = await sectionRepository.GetQueryable()
            .Where(s => sectionIds.Contains(s.Id))
            .ToListAsync();
        
        category.Sections = sections;
        
        Repository.Update(category);

        await Repository.SaveAsync();
        Logger.LogInformation("Updated category with ID: {Id}. Data: {@UpdateData}", id, category);
    }


    public async Task<IEnumerable<CategoryListResponse>> GetCategoryListAsync()
    {
        var result =await GetAllProjectedAsync<CategoryListResponse>
            (predicate:c => c.RestaurantId == user.RestaurantId,
                includes: [c => c.Translations],
                trackingBehavior: TrackingBehavior.AsNoTracking);
        return result.OrderBy(c => c.Order);
    }


    public async Task UpdateCategoryOrderAsync(List<OrderDto> dto)
    {
        var allCategoriesCount = Queryable.Count();
        if (allCategoriesCount != dto.Count)
            throw new ValidationException(Resources.WrongNumberOfObjects);
        
        var orderMap = dto.ToDictionary(d => d.Id, d => d.Order);
        
        var categoryIds = orderMap.Keys.ToList();
        var categories = await Queryable
            .Where(c => categoryIds.Contains(c.Id))
            .ToListAsync();
        
        foreach (var category in categories)
        {
            if (!orderMap.TryGetValue(category.Id, out var newOrder)) continue;
            if (category.Order != newOrder)
            {
                category.Order = newOrder;
            }
        }

        await Repository.SaveAsync();
    }
}