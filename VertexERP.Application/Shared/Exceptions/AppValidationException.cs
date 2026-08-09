namespace VertexERP.Application.Shared.Exceptions;

public class AppValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public AppValidationException(IReadOnlyList<string> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }
}