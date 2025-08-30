using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItem;

[DisplayName("آیتم منو")]
public class MenuItem: BaseEntity
{
    [Display(Name = "مسیر عکس")]
    public string? ImagePath { get; set; }

    [Display(Name = "قابل ارائه بودن")] 
    public bool IsAvailable { get; set; } = true;
    
    public ICollection<MenuItemTranslation> Translations { get; set; } = new List<MenuItemTranslation>();
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    
    public int Order { get; set; }

    public List<MenuItemVariant> Variants { get; set; } = new();
    public int? SectionId { get; set; }
    public Section? Section { get; set; }
}
