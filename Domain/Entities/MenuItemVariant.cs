using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("نوع آیتم منو")]
public class MenuItemVariant : BaseEntity
{
    public int MenuItemId { get; set; }
    public MenuItem.MenuItem MenuItem { get; set; } = null!;

    [MaxLength(150)]
    [Display(Name = "جزئیات")]
    public string Detail { get; set; }  = "";

    [Display(Name = "مبلغ")]
    public decimal Price { get; set; }

    [Display(Name = "قابل ارائه بودن")]
    public bool IsAvailable { get; set; } = true;
}
