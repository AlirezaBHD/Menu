using Application.Dto.Section;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    #region Injection

    private readonly IMenuItemRepository _menuItemRepository;

    public SectionService(ISectionRepository sectionRepository, IMapper mapper, IMenuItemRepository menuItemRepository)
        : base(mapper, sectionRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    #endregion

    public async Task<SectionResponse> CreateSectionAsync(Guid categoryId, CreateSectionRequest createSectionRequest)
    {
        var entity = Mapper.Map<CreateSectionRequest, Section>(createSectionRequest);
        entity.CategoryId = categoryId;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<Section, SectionResponse>(entity);
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
    }

    public async Task UpdateSectionAsync(Guid id, UpdateSectionRequest dto)
    {
        var section = await Repository.GetByIdAsync(id);
        section = Mapper.Map(dto, section);

        var sectionIds = dto.MenuItemIds?.Distinct().ToList() ?? [];

        var MenuItems = await _menuItemRepository.GetQueryable()
            .Where(s => sectionIds.Contains(s.Id))
            .ToListAsync();

        section.MenuItems = MenuItems;

        Repository.Update(section);
        await Repository.SaveAsync();
    }
}