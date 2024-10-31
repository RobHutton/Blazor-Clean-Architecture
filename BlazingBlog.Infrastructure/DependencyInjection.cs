using BlazorCleanArchitecture.Application.Authentication;
using BlazorCleanArchitecture.Application.Users;
using BlazorCleanArchitecture.Domain.Articles;
using BlazorCleanArchitecture.Domain.Users;
using BlazorCleanArchitecture.Infrastructure.Authentication;
using BlazorCleanArchitecture.Infrastructure.Repositories;
using BlazorCleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            AddAuthentication(services);

            services.AddHttpContextAccessor();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        private static void AddAuthentication(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddCascadingAuthenticationState();
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();
            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
        }
    }
}
