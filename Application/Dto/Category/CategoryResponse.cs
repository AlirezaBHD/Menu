using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.Section;
using Domain.Common.Attributes;

namespace Application.Dto.Category;

public class CategoryResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string? Title { get; set; }
    public ActivityPeriodResponse? ActivityPeriod { get; set; }
    public List<SectionDto> Sections { get; set; } = [];
}