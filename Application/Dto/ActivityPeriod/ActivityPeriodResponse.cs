using Domain.Entities;

namespace Application.Dto.ActivityPeriod;

public class ActivityPeriodResponse
{
    public bool IsActive { get; set; }
    public ActivityEnum ActivityEnum { get; set; }
    public string ActivityTypeName { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}