using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure
{
    public class InfrastructureConfiguration
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                    if (env.IsDevelopment())
                    {
                        options.UseSqlServer("Server=.;Database=blognetcorev2;User ID=sa;Password=1;Trusted_Connection=False;MultipleActiveResultSets=true", b => b.MigrationsAssembly("BlogNetCore"));
                    }
                    if (env.IsEnvironment("Release"))
                    {
                        options.UseSqlServer("Server=10.120.105.118,1433\\MSSQLSERVER;Database=blognetcore;User ID=sa;Password=1;Trusted_Connection=False;MultipleActiveResultSets=true", b => b.MigrationsAssembly("BlogNetCore"));
                    }
                    if (env.IsEnvironment("Debug"))
                    {
                        options.UseSqlServer("Server=.;Database=blognetcore;User ID=sa;Password=1;Trusted_Connection=False;MultipleActiveResultSets=true", b => b.MigrationsAssembly("BlogNetCore"));
                    }
                }
            );

            services.AddDefaultIdentity<BlogUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDatabaseErrorPage();
        }
    }
}
