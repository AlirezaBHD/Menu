using Muno.Application.Localization;

namespace Muno.Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "")
        : base(message)
    {
        if (string.IsNullOrEmpty(message))
            message = Resources.LoginRequired;
    }
}