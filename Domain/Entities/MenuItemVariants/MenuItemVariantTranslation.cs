using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItemVariants;

public class MenuItemVariantTranslation : BaseTranslationEntity<MenuItemVariant>
{
    [MaxLength(150)]
    [Display(Name = "جزئیات")]
    public string Detail { get; set; }  = "";

    [Display(Name = "مبلغ")]
    public decimal Price { get; set; }
}