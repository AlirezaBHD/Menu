namespace Domain.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime ModifiedOn { get; set; } = DateTime.Now;
}
