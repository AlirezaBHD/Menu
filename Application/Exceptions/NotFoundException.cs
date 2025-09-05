using Application.Localization;

namespace Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName) : base($"{entityName} {Resources.NotFound}") {}
}
