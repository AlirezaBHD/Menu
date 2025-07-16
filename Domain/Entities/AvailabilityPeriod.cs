using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AvailabilityPeriod
{
    [Display(Name = "فعال بودن")]
    public bool IsAvailable { get; set; } = true;
    
    [Display(Name = "وضعیت فعالیت")]
    public AvailabilityEnum AvailabilityType { get; set; } = AvailabilityEnum.Available;
    
    [Display(Name = "ساعت شروع")]
    public TimeOnly FromTime { get; set; } = TimeOnly.MinValue;
    
    [Display(Name = "ساعت پایان")]
    public TimeOnly ToTome { get; set; } = TimeOnly.MaxValue;
}

public enum AvailabilityEnum
{
    Available = 1,
    Unavailable = 2
}