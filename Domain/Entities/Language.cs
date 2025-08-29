namespace Domain.Entities;

public class Language : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsRtl { get; set; }
}