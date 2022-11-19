using Caramel.Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.Infrastructure.Factory
{
    public class InfrastructureFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IConfigurationSettings, ConfigurationSettings>();
        }
    }
}
