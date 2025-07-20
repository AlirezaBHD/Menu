using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.Section;

namespace Application.Dto.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<SectionDto> Sections { get; set; } = [];
}