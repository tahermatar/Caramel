<<<<<<< HEAD
﻿using Caramel.EmailService.Implementation;
=======
﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsFactory.cs" company="JustProtect">
//   Copyright (C) 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Caramel.EmailService.Implementation;
>>>>>>> development
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.EmailService.Factory
{
<<<<<<< HEAD
    public class NotificationsFactory
=======
    public static class NotificationsFactory
>>>>>>> development
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> development
