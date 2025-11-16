using System.ComponentModel;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Entities.MenuItems;
using Muno.Domain.Interfaces.Specifications;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.MenuItemVariants;

[DisplayName("نوع آیتم منو")]
public class MenuItemVariant : BaseEntity, ITranslation<MenuItemVariantTranslation>
{
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    [LocalizeDisplay(nameof(Resources.IsAvailable))]
    public bool IsAvailable { get; set; } = true;
    
    public ICollection<MenuItemVariantTranslation> Translations { get; set; } = new List<MenuItemVariantTranslation>();
}
