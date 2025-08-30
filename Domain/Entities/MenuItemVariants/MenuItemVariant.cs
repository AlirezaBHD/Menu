using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.MenuItems;

namespace Domain.Entities.MenuItemVariants;

[DisplayName("نوع آیتم منو")]
public class MenuItemVariant : BaseEntity
{
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    [Display(Name = "قابل ارائه بودن")]
    public bool IsAvailable { get; set; } = true;
    
    public ICollection<MenuItemVariantTranslation> Translations { get; set; } = new List<MenuItemVariantTranslation>();
}
