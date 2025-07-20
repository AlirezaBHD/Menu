using System.Linq.Expressions;
using Application.Dto.Section;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    #region Injection

    private readonly IMenuItemRepository _menuItemRepository;

    public SectionService(ISectionRepository sectionRepository, IMapper mapper, IMenuItemRepository menuItemRepository
        , ILogger<Section> logger)
        : base(mapper, sectionRepository, logger)
    {
        _menuItemRepository = menuItemRepository;
    }

    #endregion

    #region Availability Expression

    public static Expression<Func<Section, bool>> IsAvailable(TimeSpan nowTime)
    {
        return s =>
            s.AvailabilityPeriod.IsAvailable && (
                s.AvailabilityPeriod.AvailabilityType == AvailabilityEnum.Unlimited ||
                (
                    (s.AvailabilityPeriod.AvailabilityType == AvailabilityEnum.AvailablePeriod &&
                     nowTime >= s.AvailabilityPeriod.FromTime &&
                     nowTime <= s.AvailabilityPeriod.ToTime)
                    ||
                    (s.AvailabilityPeriod.AvailabilityType == AvailabilityEnum.UnavailablePeriod &&
                     (nowTime < s.AvailabilityPeriod.FromTime ||
                      nowTime > s.AvailabilityPeriod.ToTime))
                ));
    }

    #endregion

    public async Task<SectionResponse> CreateSectionAsync(Guid categoryId, CreateSectionRequest createSectionRequest)
    {
        var entity = Mapper.Map<CreateSectionRequest, Section>(createSectionRequest);
        entity.CategoryId = categoryId;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Section, SectionResponse>(entity);
        Logger.LogInformation("Created new section in category {CategoryId}. Data: {@UpdateData}", categoryId, entity);

        return response;
    }

    public async Task<SectionResponse> GetSectionByIdAsync(Guid sectionId)
    {
        var response =
            await GetByIdProjectedAsync<SectionResponse>(sectionId, trackingBehavior: TrackingBehavior.AsNoTracking);
        return response;
    }

    public async Task DeleteSectionAsync(Guid id)
    {
        var section = await Repository.GetByIdAsync(id);
        Repository.Remove(section);
        await Repository.SaveAsync();
        Logger.LogInformation("Deleted section with ID: {Id}", id);
    }

    public async Task UpdateSectionAsync(Guid id, UpdateSectionRequest dto)
    {
        var section = await Repository.GetQueryable()
            .Include(s => s.MenuItems).FirstAsync(s => s.Id == id);

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
}