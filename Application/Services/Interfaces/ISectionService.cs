using Application.Dto.Section;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ISectionService : IService<Section>
{
    Task<SectionResponse> CreateSectionAsync(Guid categoryId, CreateSectionRequest createSectionRequest);
    Task<SectionResponse> GetSectionByIdAsync(Guid sectionId);
    Task DeleteSectionAsync(Guid id);
}