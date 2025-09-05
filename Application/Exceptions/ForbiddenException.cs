using Application.Localization;

namespace Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "")
        : base(message)
    {
        if (string.IsNullOrEmpty(message))
            message = Resources.LoginRequired;
    }
}