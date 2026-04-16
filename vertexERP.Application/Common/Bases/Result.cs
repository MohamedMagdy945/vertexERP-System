namespace vertexERP.Application.Common.Bases
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
            => new Result(true, null);

        public static Result Failure(string error)
            => new Result(false, error);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public string? Error { get; }

        private Result(bool isSuccess, T? data, string? error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public static Result<T> Success(T value)
            => new(true, value, null);

        public static Result<T> Failure(string error)
            => new(false, default, error);
    }
}
