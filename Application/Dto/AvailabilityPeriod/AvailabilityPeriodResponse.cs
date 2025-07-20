using Domain.Entities;

namespace Application.Dto.AvailabilityPeriod;

public class AvailabilityPeriodResponse
{
    public bool IsAvailable { get; set; }
    public ActivityEnum ActivatyEnum { get; set; }
    public string AvailabilityTypeName { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}