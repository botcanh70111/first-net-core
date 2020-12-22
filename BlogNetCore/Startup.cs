using AutoMapper;
using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Implementations;
using BlogNetCore.DataServices.Implementations.Admin;
using BlogNetCore.DataServices.Implementations.Client;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using BlogNetCore.SignalRHubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;
using System;
using System.IO;

namespace BlogNetCore
{
    public class LazyLoader<T> : Lazy<T>
    {
        public LazyLoader(IServiceProvider sp) : base(sp.GetRequiredService<T>)
        {
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var servicesConfig = new ServicesConfiguration();
            Action<IMapperConfigurationExpression> mapperConfig = configure =>
            {
               configure.CreateMap<Blog, BlogFormModel>().ReverseMap();
            };

            servicesConfig.ConfigureServices(services, Configuration, WebHostEnvironment, mapperConfig);
            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //    options.AutomaticAuthentication = false;
            //});
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(options => {
                    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = "910859426143-uvqv4uglqgr96h5lghsd013thi4bfkgu.apps.googleusercontent.com";
                    options.ClientSecret = "1dlWOhd1rQ-5intVMd1y2HDZ";
                    //options.CallbackPath = "/google-signin";
                });
            services.AddSignalR();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            // Register services
            services.AddScoped<IUserManager, UserManager>();
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<IFileHandler, FileHandler>();
            services.AddTransient<IMenuViewModelService, MenuViewModelService>();
            services.AddTransient<ILayoutViewModelService, LayoutViewModelService>();
            services.AddTransient<IHomeViewModelService, HomeViewModelService>();
            services.AddTransient<IRoleViewModelService, RoleViewModelService>();
            services.AddTransient<IUserViewModelService, UserViewModelService>();
            services.AddTransient<ICategoryViewModelService, CategoryViewModelService>();
            services.AddTransient<IBlogViewModelService, BlogViewModelService>();
            services.AddTransient<IViewModelFactory, ViewModelFactory>();
            services.AddTransient(typeof(Lazy<>), typeof(LazyLoader<>));
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            Console.WriteLine("Environment Startup: " + env.EnvironmentName);
            if (env.IsDevelopment())
            {
                Console.WriteLine("ExceptionHandler Development: " + env.EnvironmentName);
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        loggerFactory.CreateLogger("Error").LogError(context.Features.Get<IExceptionHandlerFeature>().Error.Message);
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync(
                                                      "File error thrown!<br><br>\r\n");
                        }

                        await context.Response.WriteAsync(
                                                      "<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512));
                    });
                });
                //app.UseDeveloperExceptionPage();
                var servicesConfig = new ServicesConfiguration();
                servicesConfig.Configure(app, env);
            }
            else
            {
                Console.WriteLine("ExceptionHandler Release: " + env.EnvironmentName);
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        loggerFactory.CreateLogger("Error").LogError(context.Features.Get<IExceptionHandlerFeature>().Error.Message);
                    });
                });
                // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Unspecified,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "areaIdentity",
                    areaName: "Identity",
                    pattern: "Identity/{controller=Account}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "areaAdmin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "Client",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "blogs",
                    areaName: "Client",
                    pattern: "blogs/{*slug}",
                    defaults: new {controller = "Blogs", action= "Index"}
                    );
                endpoints.MapControllerRoute(
                    name: "common",
                    pattern: "request/{controller}/{action}/{id?}",
                    defaults: new { controller = "FileManager", action = "GetBlogFiles" }
                    );

                endpoints.MapRazorPages();
                endpoints.MapHub<AdminNotificationHub>("/admin/notification");
                endpoints.MapHub<OnlineHub>("/client/online");
                endpoints.MapHub<ChatHub>("/client/chat");
            });
        }
    }
}
