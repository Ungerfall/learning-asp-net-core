using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindStore.ComponentModel.Design;
using NorthwindStore.Data;
using NorthwindStore.Data.Filters;
using NorthwindStore.Data.Models;
using NorthwindStore.Filters;
using NorthwindStore.Identity;
using NorthwindStore.IO;
using NorthwindStore.Middleware;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NorthwindStore
{
    public class Startup
    {
        private const string PRODUCTS_CONFIGURATION_SECTION = "Products";
        private const string FILE_CACHE_CONFIGURATION_SECTION = "FileCache";
        private const string LOGGING_FILTER_CONFIGURATION_SECTION = "LoggingFilter";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("NorthwindContext")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<NorthwindContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            });
            var mvcBuilder = services.AddRazorPages();
#if DEBUG
            if (Env.IsDevelopment())
                mvcBuilder.AddRazorRuntimeCompilation();
#endif

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options =>
                {
                    Configuration.Bind("AzureAd", options);
                    options.CookieSchemeName = IdentityConstants.ExternalScheme;
                });

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

                options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            });

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
            services.AddSingleton<ProductFilter>(x =>
            {
                var productCfg = new ProductFilter();
                Configuration.GetSection(PRODUCTS_CONFIGURATION_SECTION).Bind(productCfg);
                return productCfg;
            });
            services.AddSingleton<LoggingFilterConfiguration>(container =>
            {
                var loggingFilterConfig = new LoggingFilterConfiguration();
                Configuration.GetSection(LOGGING_FILTER_CONFIGURATION_SECTION).Bind(loggingFilterConfig);

                return loggingFilterConfig;
            });
            services.AddScoped<LoggingFilter>();
            services.AddSingleton<ILinkedBreadcrumbsFactory, LinkedBreadcrumbsFactory>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
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
