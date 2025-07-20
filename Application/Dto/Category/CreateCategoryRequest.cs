using Application.Dto.AvailabilityPeriod;

namespace Application.Dto.Category;

public class CreateCategoryRequest
{
    public string Title { get; set; }
    public AvailabilityPeriodRequest AvailabilityPeriod { get; set; }
}