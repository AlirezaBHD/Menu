using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ActivityPeriod
{
    [Display(Name = "فعال بودن")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "وضعیت فعالیت")]
    public ActivityEnum ActivityType { get; set; } = ActivityEnum.Unlimited;
    
    [Display(Name = "ساعت شروع")]
    public TimeSpan FromTime { get; set; } = TimeSpan.Zero;
    
    [Display(Name = "ساعت پایان")]
    public TimeSpan ToTime { get; set; } = TimeSpan.FromHours(24);
}

public enum ActivityEnum
{
    [Display(Name = "بدون محدودیت")]
    Unlimited = 0,
    
    [Display(Name = "بازه فعال بودن")]
    ActivePeriod = 1,
    
    [Display(Name = "بازه غیرفعال بودن")]
    InactivePeriod = 2
}