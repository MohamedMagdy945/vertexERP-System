namespace VertexERP.Application.Common.Exceptions
{
    public class ValidationAppException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }
        public ValidationAppException(string message, Dictionary<string, List<string>> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
