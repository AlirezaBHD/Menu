using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AvailabilityPeriod
{
    [Display(Name = "فعال بودن")]
    public bool IsAvailable { get; set; } = true;
    
    [Display(Name = "وضعیت فعالیت")]
    public AvailabilityEnum AvailabilityType { get; set; } = AvailabilityEnum.Unlimited;
    
    [Display(Name = "ساعت شروع")]
    public TimeSpan FromTime { get; set; } = TimeSpan.Zero;
    
    [Display(Name = "ساعت پایان")]
    public TimeSpan ToTime { get; set; } = TimeSpan.FromHours(24);
}

public enum AvailabilityEnum
{
    [Display(Name = "بدون محدودیت")]
    Unlimited = 0,
    
    [Display(Name = "بازه فعال بودن")]
    AvailablePeriod = 1,
    
    [Display(Name = "بازه غیرفعال بودن")]
    UnavailablePeriod = 2
}