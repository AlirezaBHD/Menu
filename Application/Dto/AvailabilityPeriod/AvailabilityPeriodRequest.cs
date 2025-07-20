using Domain.Entities;

namespace Application.Dto.AvailabilityPeriod;

public class AvailabilityPeriodRequest
{
    public bool IsAvailable { get; set; }
    public AvailabilityEnum AvailabilityType { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}