using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("رستوران")]
public class Restaurant: BaseEntity
{
    [Display(Name = "نام")]
    [MaxLength(50)]
    public required string Name { get; set; }
    
    public Guid OwnerId { get; set; }
    
    [Display(Name = "نشانی")]
    [MaxLength(500)]
    public required string Address { get; set; }
    
    [Display(Name = "توضیحات")]
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [Display(Name = "لوگو")]
    public string? LogoPath { get; set; }
    
    [Display(Name = "ساعت های کاری مجموعه")]
    public Dictionary<string, string> OpeningHours { get; set; } = new();
    
    public User? Owner { get; set; }
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
