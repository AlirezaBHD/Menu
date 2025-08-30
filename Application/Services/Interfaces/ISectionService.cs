using Application.Dto.Section;
using Application.Dto.Shared;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ISectionService : IService<Section>
{
    Task<SectionResponse> CreateSectionAsync(int categoryId, CreateSectionRequest createSectionRequest);
    Task<SectionResponse> GetSectionByIdAsync(int sectionId);
    Task DeleteSectionAsync(int id);
    Task UpdateSectionAsync(int id, UpdateSectionRequest dto);
    Task<IEnumerable<SectionListResponse>> GetSectionListAsync();
    Task UpdateSectionOrderAsync(List<OrderDto> dto);
}