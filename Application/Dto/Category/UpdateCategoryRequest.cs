using Application.Dto.ActivityPeriod;

namespace Application.Dto.Category;

public class UpdateCategoryRequest
{
    public string Title { get; set; }
    public List<int> SectionIds { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
}