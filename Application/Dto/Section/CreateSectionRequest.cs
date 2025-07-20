using Application.Dto.ActivityPeriod;

namespace Application.Dto.Section;

public class CreateSectionRequest
{
    public string? Title { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
}