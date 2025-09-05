using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities.MenuItemVariants;

public class MenuItemVariantTranslation : BaseTranslationEntity<MenuItemVariant>
{
    [MaxLength(150)]
    [LocalizeDisplay(nameof(Resources.Detail))]
    public string Detail { get; set; }  = "";

    [LocalizeDisplay(nameof(Resources.Price))]
    public decimal Price { get; set; }
}