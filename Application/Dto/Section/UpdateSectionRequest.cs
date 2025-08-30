using Application.Dto.ActivityPeriod;

namespace Application.Dto.Section;

public class UpdateSectionRequest
{
    public string Title { get; set; }
    public List<int> MenuItemIds { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
}