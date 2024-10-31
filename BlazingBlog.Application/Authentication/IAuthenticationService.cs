namespace BlazorCleanArchitecture.Application.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisterUserResponse> RegisterUserAsync(string username, string email, string password);
        Task<bool> LoginUserAsync(string username, string password);
        Task LogoutUserAsync();
    }
}
