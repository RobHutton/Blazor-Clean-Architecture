namespace BlazingBlog.Application.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException() : base() { }
        public UserNotAuthorizedException(string message) : base(message) { }
        public UserNotAuthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
