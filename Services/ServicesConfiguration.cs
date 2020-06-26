using AutoMapper;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;

namespace Services
{
    public class ServicesConfiguration
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureConfig = new InfrastructureConfiguration();
            infrastructureConfig.ConfigureServices(services, configuration);

            // Register service
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.CreateMap<IdentityRole, Role>().ReverseMap();
                mc.CreateMap<User, User>().ReverseMap()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                mc.CreateMap<BlogUser, User>().ReverseMap()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                mc.CreateMap<IdentityUserClaim<string>, UserClaim>().ReverseMap();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IModelMapper, ModelMapper>();
            services.AddTransient<ISiteConfigService, SiteConfigService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPasswordHasher<BlogUser>, PasswordHasher<BlogUser>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var infrastructureConfig = new InfrastructureConfiguration();
            infrastructureConfig.Configure(app, env);
        }
    }
}
