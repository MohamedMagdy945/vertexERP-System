namespace VertexERP.Application.Common.Bases
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; } = new();
        public int StatusCode { get; set; }
        public string? CorrelationId { get; set; }

        public Response() { }

        public Response(T data, string message = "Request successful", int statusCode = 200)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }
        public Response(string message, Dictionary<string, List<string>>? errors = null, int statusCode = 400)
        {
            IsSuccess = false;
            Message = message;
            Errors = errors ?? new();
            StatusCode = statusCode;
        }
    }
}
