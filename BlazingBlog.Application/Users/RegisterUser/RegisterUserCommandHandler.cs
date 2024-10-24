
using BlazingBlog.Application.Authentication;

namespace BlazingBlog.Application.Users.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        public RegisterUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RegisterUserAsync(request.UserName, request.UserEmail, request.Password);
            if (result.Succeeded)
            {
                Result.OK();
            }
            return Result.ERR($"{string.Join(", ", result.Errors)}");
        }
    }
}
