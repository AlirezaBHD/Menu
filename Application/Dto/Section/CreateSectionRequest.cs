namespace Application.Dto.Section;

public class CreateSectionRequest
{
    public Guid CategoryId { get; set; }
    public string? Title { get; set; }
}