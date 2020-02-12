using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace NorthwindStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application location: {0}", AppDomain.CurrentDomain.BaseDirectory);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
#if DOCKER
                .ConfigureAppConfiguration(builder => { builder.AddJsonFile("appsettings.Docker.json"); })
#endif
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging((ctx, builder) =>
                    {
                        builder.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                        builder.AddFile(o => o.RootPath = ctx.HostingEnvironment.ContentRootPath);
                    });
                });
    }
}
