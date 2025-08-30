using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItemVariants;

public class MenuItemVariantTranslation : BaseEntity
{
    [MaxLength(150)]
    [Display(Name = "جزئیات")]
    public string Detail { get; set; }  = "";

    [Display(Name = "مبلغ")]
    public decimal Price { get; set; }
    
    public int MenuItemVariantId { get; set; }
    public MenuItemVariant MenuItemVariant { get; set; } = default!;
    
    [MaxLength(10)]
    public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}