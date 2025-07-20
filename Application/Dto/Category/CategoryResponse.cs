using System.ComponentModel.DataAnnotations;
using Application.Dto.AvailabilityPeriod;
using Application.Dto.Section;

namespace Application.Dto.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public AvailabilityPeriodResponse AvailabilityPeriod { get; set; }
    public List<SectionDto> Sections { get; set; } = [];
}