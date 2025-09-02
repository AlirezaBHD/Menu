using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Application.Dto.MenuItem;

public class MenuItemResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
}