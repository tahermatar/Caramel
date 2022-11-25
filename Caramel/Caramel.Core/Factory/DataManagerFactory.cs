using Caramel.EmailService.Factory;
using Caramel.Infrastructure.Factory;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Caramel.Core.Factory
{
    public class DataManagerFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            InfrastructureFactory.RegisterDependencies(services);
            NotificationsFactory.RegisterDependencies(services);

            Assembly assembly = typeof(DataManagerFactory).GetTypeInfo().Assembly;

            var allManagers = assembly.GetTypes().Where(t => t.Name.EndsWith("Manager"));

            foreach (var type in allManagers)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
                foreach (var itype in mainInterfaces)
                {
                    services.AddScoped(itype, type);
                }
            }
        }
    }
}
