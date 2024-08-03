using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTracking.Auth;
using SalesTracking.Business;
using SalesTracking.Data;
using SalesTracking.Entities.Auth;

namespace SalesTracking.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationServiceCollection.RegisterServices(services, configuration);
            BusinessServiceCollection.RegisterServices(services);
            DataServiceCollection.RegisterServices(services, configuration);
            return services;
        }
    }
}
