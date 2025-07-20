using Application.Dto.ActivityPeriod;

namespace Application.Dto.Category;

public class CreateCategoryRequest
{
    public string Title { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
}