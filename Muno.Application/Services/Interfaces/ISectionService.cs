using Muno.Domain.Entities;
using Muno.Domain.Entities.Sections;
using Muno.Application.Dto.Section;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Services.Interfaces;

public interface ISectionService : IService<Section>
{
    Task<SectionResponse> CreateSectionAsync(int categoryId, CreateSectionRequest createSectionRequest);
    Task<SectionResponse> GetSectionByIdAsync(int sectionId);
    Task DeleteSectionAsync(int id);
    Task UpdateSectionAsync(int id, int categoryId, UpdateSectionRequest dto);
    Task<IEnumerable<SectionListResponse>> GetSectionListAsync();
    Task UpdateSectionOrderAsync(List<OrderDto> dto);
}