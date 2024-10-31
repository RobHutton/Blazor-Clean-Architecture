namespace BlazorCleanArchitecture.Application.Users.GetUsers
{
    public class GetUserRolesQuery : IQuery<List<string>>
    {
        public required string UserId { get; set; }
    }
}
