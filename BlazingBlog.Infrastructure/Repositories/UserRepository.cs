using BlazingBlog.Domain.Users;
using BlazingBlog.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazingBlog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        public UserRepository(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<IUser>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(user => (IUser)user)
                .ToListAsync();
        }

        public async Task<IUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<IUser>> GetUsersByIdsAsync(IEnumerable<string> userIds)
        {
            return await _userManager.Users
                .Select(user => (IUser)user)
                .Where(u => userIds.Contains(u.Id))
                .Cast<IUser>()
                .ToListAsync();
        }
    }
}
