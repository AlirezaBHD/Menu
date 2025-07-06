namespace Domain.Interfaces.Services;

public interface ICurrentUser
{
    Guid? UserId { get; }
}
