using Caramel.EmailService.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.EmailService.Factory
{
    public class NotificationsFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
