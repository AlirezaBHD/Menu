using Muno.Application.Localization;

namespace Muno.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName) : base($"{entityName} {Resources.NotFound}") {}
}
