using System.Linq.Expressions;
using Application.Dto.Section;
using Application.Dto.Shared;
using Application.Exceptions;
using Application.Localization;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Sections;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    #region Injection

    private readonly IMenuItemRepository _menuItemRepository;
    private readonly ICurrentLanguage _currentLanguage;

    public SectionService(ISectionRepository sectionRepository, IMapper mapper, IMenuItemRepository menuItemRepository
        , ILogger<Section> logger, ICurrentLanguage currentLanguage)
        : base(mapper, sectionRepository, logger)
    {
        _menuItemRepository = menuItemRepository;
        _currentLanguage = currentLanguage;
    }

    #endregion

    #region Activity Expression

    public static Expression<Func<Section, bool>> IsAvailable(TimeSpan nowTime)
    {
        return s =>
            s.ActivityPeriod.IsActive && (
                s.ActivityPeriod.ActivityType == ActivityEnum.Unlimited ||
                (
                    (s.ActivityPeriod.ActivityType == ActivityEnum.ActivePeriod &&
                     nowTime >= s.ActivityPeriod.FromTime &&
                     nowTime <= s.ActivityPeriod.ToTime)
                    ||
                    (s.ActivityPeriod.ActivityType == ActivityEnum.InactivePeriod &&
                     (nowTime < s.ActivityPeriod.FromTime ||
                      nowTime > s.ActivityPeriod.ToTime))
                ));
    }

    #endregion

    public async Task<SectionResponse> CreateSectionAsync(int categoryId, CreateSectionRequest createSectionRequest)
    {
        var entity = Mapper.Map<CreateSectionRequest, Section>(createSectionRequest);
        
        entity.CategoryId = categoryId;
        
        var count = Queryable.Count();
        entity.Order = count + 1;
        
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Section, SectionResponse>(entity);
        Logger.LogInformation("Created new section in category {CategoryId}. Data: {@UpdateData}", categoryId, entity);

        return response;
    }

    public async Task<SectionResponse> GetSectionByIdAsync(int sectionId)
    {
        var query = Queryable.Include(s => s.Translations)
            .Include(s => s.MenuItems).ThenInclude(m => m.Translations);
        
        var response =
            await GetByIdProjectedAsync<SectionResponse>(sectionId,
                query: query,
                trackingBehavior: TrackingBehavior.AsNoTracking);
        
        return response;
    }

    public async Task DeleteSectionAsync(int id)
    {
        var section = await Repository.GetByIdAsync(id);
        Repository.Remove(section);
        await Repository.SaveAsync();
        Logger.LogInformation("Deleted section with ID: {Id}", id);
    }

    public async Task UpdateSectionAsync(int id, UpdateSectionRequest dto)
    {
        var section = await Queryable
            .Include(s => s.MenuItems)
            .Include(s => s.Translations)
            .FirstAsync(s => s.Id == id);

        section = Mapper.Map(dto, section);

        var sectionIds = dto.MenuItemIds?.Distinct().ToList() ?? [];

        var menuItems = await _menuItemRepository.GetQueryable()
            .Where(s => sectionIds.Contains(s.Id))
            .ToListAsync();

        section.MenuItems = menuItems;

        Repository.Update(section);
        await Repository.SaveAsync();
        Logger.LogInformation("Updated section with ID {Id}. Data: {@UpdateData}", id, section);
    }

    public async Task<IEnumerable<SectionListResponse>> GetSectionListAsync()
    {

        var query =  Queryable.Include(s => s.Translations)
            .Include(s => s.Category).ThenInclude(c => c.Translations);
        var result =await GetAllProjectedAsync<SectionListResponse>( query:query,
            // includes: [s => s.Translations],
            trackingBehavior:TrackingBehavior.AsNoTracking);
        
        // var data = await Queryable
        //     .Include(s => s.Category).ThenInclude(c => c.Translations)
        //     .ToListAsync();
        //
        // var result = Mapper.Map<List<SectionListResponse>>(data, opt =>
        // {
        //     opt.Items["CurrentLanguage"] = _currentLanguage.GetLanguage();
        // });

        
        return  result.OrderBy(s => s.Order);
    }

    public async Task UpdateSectionOrderAsync(List<OrderDto> dto)
    {
        var allSectionsCount = Queryable.Count();
        if (allSectionsCount != dto.Count)
            throw new ValidationException(Resources.WrongNumberOfObjects);
        
        var orderMap = dto.ToDictionary(d => d.Id, d => d.Order);

        var sectionIds = orderMap.Keys.ToList();
        var sections = await Queryable
            .Where(c => sectionIds.Contains(c.Id))
            .ToListAsync();
        
        foreach (var section in sections)
        {
            if (!orderMap.TryGetValue(section.Id, out var newOrder)) continue;
            if (section.Order != newOrder)
            {
                section.Order = newOrder;
            }
        }

        await Repository.SaveAsync();
    }
}