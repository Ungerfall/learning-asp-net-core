using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindStore.Data;
using NorthwindStore.Data.Filters;
using NorthwindStore.Data.Models;
using NorthwindStore.IO;
using NorthwindStore.Middleware;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace NorthwindStore
{
    public class Startup
    {
        private const string PRODUCTS_CONFIGURATION_SECTION = "Products";
        private const string FILE_CACHE_CONFIGURATION_SECTION = "FileCache";
        private const string LOGGING_FILTER_CONFIGURATION_SECTION = "LoggingFilter";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("NorthwindContext")));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddSingleton<IFileCache>(x =>
            {
                var cacheConfig = new FileCacheConfiguration();
                Configuration.GetSection(FILE_CACHE_CONFIGURATION_SECTION).Bind(cacheConfig);
                cacheConfig.Dir = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty,
                    cacheConfig.Dir);
                var fileCache = new AdamCarterFileCacheWrapper(cacheConfig);

                return fileCache;
            });
            {
                var productCfg = new ProductFilter();
                Configuration.GetSection(PRODUCTS_CONFIGURATION_SECTION).Bind(productCfg);
                return productCfg;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exceptionPath = exceptionHandlerPathFeature?.Path;
                        var exceptionError = exceptionHandlerPathFeature?.Error.Message;

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync(
                            $"ERROR!<br>Ask support team for help. Error path = {exceptionPath}<br>\r\n");

                        logger.LogError("Path: {0}; Error: {1}", exceptionPath, exceptionError);

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                */
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseImageCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            logger.LogInformation("Configuration: {0}", ConfigurationString);
        }

        private string ConfigurationString => string.Join(
            Environment.NewLine,
            Configuration
                .AsEnumerable()
                .Select(x => $"{x.Key}: {x.Value}"));
    }
}
