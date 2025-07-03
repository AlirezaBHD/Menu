using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("آیتم منو")]
public class MenuItem: BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    
    [MaxLength(300)]
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    
    [Display(Name = "مبلغ")]
    public decimal Price { get; set; }
    
    [Display(Name = "مسیر عکس")]
    public string? ImagePath { get; set; }
    
    [Display(Name = "فعال بودن")]
    public bool IsAvailable { get; set; } = true;
    public Guid? SectionId { get; set; }
    
    public Section? Section { get; set; }
}
