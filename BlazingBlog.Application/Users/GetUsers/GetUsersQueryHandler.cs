using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Users.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }
        public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (!await _userService.IsCurrentUserInRoleAsync("Admin"))
            {
                return Result.Fail<List<UserDto>>("You are not authorized to view all users.");
            }
            var users = await _userRepository.GetAllUsersAsync();
            var response = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = user.Adapt<UserDto>();
                userDto.Roles = string.Join(", ", await _userService.GetUserRolesAsync(user.Id));
                response.Add(userDto);
            }
            return response;
        }
    }
}
