using Application.Dto.Section;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    #region Injection

    public SectionService(ISectionRepository sectionRepository, IMapper mapper) 
        : base(mapper, sectionRepository)
    {
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
        await Repository.SaveAsync();    }
}