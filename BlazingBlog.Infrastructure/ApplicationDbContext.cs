using BlazorCleanArchitecture.Domain.Articles;
using BlazorCleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorCleanArchitecture.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
    }
}
