using Application.Dto.Section;
using Application.Dto.Shared;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ISectionService : IService<Section>
{
    Task<SectionResponse> CreateSectionAsync(Guid categoryId, CreateSectionRequest createSectionRequest);
    Task<SectionResponse> GetSectionByIdAsync(Guid sectionId);
    Task DeleteSectionAsync(Guid id);
    Task UpdateSectionAsync(Guid id, UpdateSectionRequest dto);
    Task<IEnumerable<SectionListResponse>> GetSectionListAsync();
    Task UpdateSectionOrderAsync(List<OrderDto> dto);
}