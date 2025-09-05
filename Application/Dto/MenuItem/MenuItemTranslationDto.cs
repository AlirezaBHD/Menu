using Application.Dto.Shared;
using Domain.Common.Attributes;

namespace Application.Dto.MenuItem;

public class MenuItemTranslationDto : TranslationDto
{
    [MultiLanguageProperty] 
    public string Title { get; set; }
    
    [MultiLanguageProperty] 
    public string? Description { get; set; }
}