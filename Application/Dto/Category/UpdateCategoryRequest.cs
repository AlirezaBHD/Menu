using Application.Dto.AvailabilityPeriod;

namespace Application.Dto.Category;

public class UpdateCategoryRequest
{
    public string Title { get; set; }
    public List<Guid> SectionIds { get; set; }
    public AvailabilityPeriodRequest AvailabilityPeriod { get; set; }
}