namespace VertexERP.Application.Shared.Exceptions;

public class AppDuplicateEntityException : Exception
{
    public AppDuplicateEntityException(string message = "Entity already exists.", Exception? innerException = null)
        : base(message, innerException)
    {
    }
}