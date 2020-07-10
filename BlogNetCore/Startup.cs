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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Models;
using System;

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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var servicesConfig = new ServicesConfiguration();
            Action<IMapperConfigurationExpression> mapperConfig = configure =>
            {
               configure.CreateMap<Blog, BlogFormModel>().ReverseMap();
            };

            servicesConfig.ConfigureServices(services, Configuration, mapperConfig);
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var servicesConfig = new ServicesConfiguration();
                servicesConfig.Configure(app, env);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
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
