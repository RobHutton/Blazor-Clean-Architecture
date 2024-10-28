
namespace BlazingBlog.Application.Users.AddRoleToUser
{
    public class AddRoleToUserCommandHandler : ICommandHandler<AddRoleToUserCommand>
    {
        private readonly IUserService _userService;
        public AddRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Result> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.AddRoleToUserAsync(request.UserId, request.RoleName);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            return Result.OK();
        }
    }
}
