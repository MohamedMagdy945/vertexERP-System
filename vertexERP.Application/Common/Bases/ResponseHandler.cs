using Microsoft.AspNetCore.Http;


namespace VertexERP.Application.Common.Bases
{
    public static class ResponseHandler
    {

        public static Response<T> Success<T>(T data, string message = "Request successful", int statusCode = StatusCodes.Status200OK)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static Response<T> Failure<T>(string message,
            Dictionary<string, List<string>>? errors = null,
            int statusCode = StatusCodes.Status400BadRequest,
                string? correlationId = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new Dictionary<string, List<string>>(),
                StatusCode = statusCode,
                CorrelationId = correlationId
            };
        }

        public static Response<T> NotFound<T>(string message = "Resource not found",
            Dictionary<string, List<string>>? errors = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new Dictionary<string, List<string>>(),
                StatusCode = StatusCodes.Status404NotFound
            };
        }
        public static Response<T> Unauthorized<T>(string message = "Unauthorized")
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = new Dictionary<string, List<string>>(),
                StatusCode = 401
            };
        }
    }
}
