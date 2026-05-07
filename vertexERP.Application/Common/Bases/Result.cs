namespace VertexERP.Application.Common.Bases
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
            => new(true, string.Empty);

        public static Result Failure(string error)
            => new(false, error);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public T? Data { get; }
        public string Error { get; }

        private Result(bool isSuccess, T? value, string error)
        {
            IsSuccess = isSuccess;
            Data = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new(true, value, string.Empty);
        }

        public static Result<T> Failure(string error)
            => new(false, default, error);
    }
}
