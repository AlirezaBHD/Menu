using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;

namespace Muno.Application.Dto.MenuItem;

public class MenuItemResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
}