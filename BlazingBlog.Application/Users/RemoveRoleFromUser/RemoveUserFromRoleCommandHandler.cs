
namespace BlazingBlog.Application.Users.RemoveRoleFromUser
{
    public class RemoveUserFromRoleCommandHandler : ICommandHandler<RemoveRoleFromUserCommand>
    {
        private readonly IUserService _userService;
        public RemoveUserFromRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            return Result.OK();
        }
    }
}
