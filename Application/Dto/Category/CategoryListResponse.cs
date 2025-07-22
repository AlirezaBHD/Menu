using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Category;

public class CategoryListResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int Order { get; set; }
}