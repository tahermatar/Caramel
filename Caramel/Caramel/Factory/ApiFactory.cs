using Caramel.Core.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.Factory
{
    public class ApiFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            DataManagerFactory.RegisterDependencies(services);
        }
    }
}
