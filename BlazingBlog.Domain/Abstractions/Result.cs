namespace BlazingBlog.Domain.Abstractions
{
    public class Result
    {
        public bool Success { get; set; }
        public bool Failure => !Success;
        public string? Error { get; }
        protected Result(bool success, string? error = null)
        {
            Success = success;
            Error = error;
        }
        public static Result OK() => new(true);
        public static Result Fail(string error) => new(false, error);
        public static Result<T> OK<T>(T value) => new(value, true, string.Empty);
        public static Result<T> Fail<T>(string error) => new(default, false, error);
        // Convert value to result
        public static Result<T> FromValue<T>(T? value) => value != null ? OK(value) : Fail<T>("Provided value is null.");
    }
    public class Result<T> : Result
    {
        public T? Value { get; }
        internal Result(T? value, bool ok, string error)
            : base(ok, error)
        {
            Value = value;
        }
        // Implicitly converts a value to a Result<value>
        public static implicit operator Result<T>(T value) => FromValue(value);
        // Implicitly converts a Result<value> to a value
        public static implicit operator T?(Result<T> result) => result.Value;
    }
}
