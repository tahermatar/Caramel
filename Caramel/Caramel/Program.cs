using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
using System.IO;
=======
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> development

namespace Caramel
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
<<<<<<< HEAD
                           .ConfigureWebHostDefaults(webHostBuilder =>
                           {
                               webHostBuilder
                                      .UseContentRoot(Directory.GetCurrentDirectory())
                                      .UseIISIntegration()
                                      .UseStartup<Startup>();
                           })
                           .Build();
            host.Run();
            return 0;
=======
                           .ConfigureWebHostDefaults(webHostBuilder => {
                               webHostBuilder
                                     .UseContentRoot(Directory.GetCurrentDirectory())
                                     .UseIISIntegration()
                                     .UseStartup<Startup>();
                           })
                           .Build();
            host.Run();
>>>>>>> development
        }
    }
}
