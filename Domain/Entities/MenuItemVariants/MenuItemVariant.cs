using System.ComponentModel;
using Domain.Common.Attributes;
using Domain.Entities.MenuItems;
using Domain.Interfaces.Specifications;
using Domain.Localization;

namespace Domain.Entities.MenuItemVariants;

[DisplayName("نوع آیتم منو")]
public class MenuItemVariant : BaseEntity, ITranslation<MenuItemVariantTranslation>
{
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    [LocalizeDisplay(nameof(Resources.IsAvailable))]
    public bool IsAvailable { get; set; } = true;
    
    public ICollection<MenuItemVariantTranslation> Translations { get; set; } = new List<MenuItemVariantTranslation>();
}
