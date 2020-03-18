using Microsoft.Extensions.DependencyInjection;

namespace Client.Services
{
    public static class ServiceCollectionServicesExtensions
    {
        public static IServiceCollection AddTokenServices(this IServiceCollection services)
        {
            return services.AddSingleton<ITokenStore, TokenStore>();
        }

        public static IServiceCollection AddTypingTracking(this IServiceCollection services)
        {
            return services.AddSingleton<TypingTracking>();
        }
    }
}