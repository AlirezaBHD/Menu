using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.MenuItemVariants;

public class MenuItemVariantTranslation : BaseTranslationEntity<MenuItemVariant>
{
    [MaxLength(150)]
    [LocalizeDisplay(nameof(Resources.Detail))]
    public string Detail { get; set; }  = "";

    [LocalizeDisplay(nameof(Resources.Price))]
    public string Price { get; set; }  = "0";
}