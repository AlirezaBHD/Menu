using Domain.Entities;

namespace Application.Dto.ActivityPeriod;

public class ActivityPeriodRequest
{
    public bool IsAvailable { get; set; }
    public ActivityEnum ActivityEnum { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}