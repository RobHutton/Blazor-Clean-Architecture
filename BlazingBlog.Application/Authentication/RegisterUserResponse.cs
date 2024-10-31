namespace BlazorCleanArchitecture.Application.Authentication
{
    public class RegisterUserResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
