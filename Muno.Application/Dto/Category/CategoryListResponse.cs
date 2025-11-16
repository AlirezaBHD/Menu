using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;

namespace Muno.Application.Dto.Category;

public class CategoryListResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public int Order { get; set; }
}