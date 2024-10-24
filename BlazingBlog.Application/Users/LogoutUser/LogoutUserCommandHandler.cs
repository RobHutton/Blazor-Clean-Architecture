
using BlazingBlog.Application.Authentication;

namespace BlazingBlog.Application.Users.LogoutUser
{
    public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        public LogoutUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.LogoutUserAsync();
            return Result.OK();
        }
    }
}
