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
            return services;
        }
    }
}
