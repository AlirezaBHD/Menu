using System.Linq.Expressions;
using Application.Dto.Category;
using Application.Dto.Shared;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Categories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    #region Injection

    private readonly ISectionRepository _sectionRepository;
    private readonly ICurrentUser _user;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ISectionRepository sectionRepository, ILogger<Category> logger, ICurrentUser user)
        : base(mapper, categoryRepository, logger)
    {
        _sectionRepository = sectionRepository;
        _user = user;
    }

    #endregion

    #region Activity Expression

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

    #endregion
    
    public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest)
    {
        
        var entity = Mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        
        entity.RestaurantId = _user.RestaurantId;
        
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
        var response =
            await GetByIdProjectedAsync<CategoryResponse>(categoryId, trackingBehavior: TrackingBehavior.AsNoTracking);
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

    public async Task<IEnumerable<CategoryListResponse>> GetCategoryListAsync()
    {
        var result =await GetAllProjectedAsync<CategoryListResponse>
            (predicate:c => c.RestaurantId == _user.RestaurantId,trackingBehavior: TrackingBehavior.AsNoTracking);
        return result.OrderBy(c => c.Order);
    }

    public async Task UpdateCategoryOrderAsync(List<OrderDto> dto)
    {
        var allCategoriesCount = Queryable.Count();
        if (allCategoriesCount != dto.Count)
            throw new ValidationException("تعداد آبجکت های ورودی با تعداد آبجکت های موجود مغایرت دارد");
        
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