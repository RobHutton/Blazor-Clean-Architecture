using BlazorCleanArchitecture.Application.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });
            services.AddScoped<IArticlesViewService, ArticlesViewService>();
            return services;
        }
    }
}
