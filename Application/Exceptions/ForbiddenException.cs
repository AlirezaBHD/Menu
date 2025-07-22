namespace Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "نیاز به ورود مجدد است") 
        : base(message) { }
}