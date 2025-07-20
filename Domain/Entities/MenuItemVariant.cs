using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("نوع آیتم منو")]
public class MenuItemVariant : BaseEntity
{
    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    [MaxLength(100)]
    [Display(Name = "عنوان نوع")]
    public string Title { get; set; } = "";

    [MaxLength(300)]
    [Display(Name = "توضیحات")]
    public string Description { get; set; }  = "";

    [Display(Name = "قیمت")]
    public decimal Price { get; set; }

    [Display(Name = "موجودی")]
    public bool IsAvailable { get; set; } = true;
}
