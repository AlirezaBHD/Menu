using Muno.Domain.Common.Attributes;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities;

public class ActivityPeriod
{
    [LocalizeDisplay(nameof(Resources.IsActive))]
    public bool IsActive { get; set; } = true;

    [LocalizeDisplay(nameof(Resources.ActivityType))]
    public ActivityEnum ActivityType { get; set; } = ActivityEnum.Unlimited;

    [LocalizeDisplay(nameof(Resources.FromTime))]
    public TimeSpan FromTime { get; set; } = TimeSpan.Zero;

    [LocalizeDisplay(nameof(Resources.ToTime))]
    public TimeSpan ToTime { get; set; } = TimeSpan.FromHours(24);
}

public enum ActivityEnum
{
    [LocalizeDisplay(nameof(Resources.Unlimited))]
    Unlimited = 0,

    [LocalizeDisplay(nameof(Resources.ActivePeriod))]
    ActivePeriod = 1,

    [LocalizeDisplay(nameof(Resources.InactivePeriod))]
    InactivePeriod = 2
}