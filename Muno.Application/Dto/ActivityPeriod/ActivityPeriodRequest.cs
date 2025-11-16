using Domain.Entities;

namespace Muno.Application.Dto.ActivityPeriod;

public class ActivityPeriodRequest
{
    public bool IsActive { get; set; }
    public ActivityEnum ActivityType { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}