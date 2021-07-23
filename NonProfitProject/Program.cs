using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NonProfitProject.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject
{
    public class Program
    {
        public static void Main(string[] args)
       {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseWebRoot("wwwroot");
                    webBuilder.UseStartup<Startup>().UseDefaultServiceProvider(options => options.ValidateScopes = false);
                }).ConfigureServices(services =>
                {
                    services.AddHostedService<BackgroundTasks>();
                });
    }
}
