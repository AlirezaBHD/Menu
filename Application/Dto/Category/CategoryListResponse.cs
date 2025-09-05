using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Application.Dto.Category;

public class CategoryListResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public int Order { get; set; }
}