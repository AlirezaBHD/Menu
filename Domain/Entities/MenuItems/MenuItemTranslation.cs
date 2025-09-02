using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItems;

public sealed class MenuItemTranslation : BaseTranslationEntity<MenuItem>
{
    [MaxLength(50)]
    public required string Title { get; set; }
    
    [MaxLength(300)]
    public string? Description { get; set; }
}
