// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsFactory.cs" company="JustProtect">
//   Copyright (C) 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Caramel.EmailService.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.EmailService.Factory
{
    public static class NotificationsFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}