using Application.Dto.AvailabilityPeriod;

namespace Application.Dto.Section;

public class CreateSectionRequest
{
    public string? Title { get; set; }
    public AvailabilityPeriodRequest AvailabilityPeriod { get; set; }
}