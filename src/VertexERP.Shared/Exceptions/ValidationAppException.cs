namespace VertexERP.Shared.Exceptions;

public sealed class ValidationAppException : Exception
{
    public string[] Errors { get; }

    public ValidationAppException(string[] errors, string message = "Validation failed.")
        : base(message)
    {
        Errors = errors;
    }
}

