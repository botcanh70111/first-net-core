﻿using AutoMapper;
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
using System;

namespace Services
{
    public class ServicesConfiguration
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env, Action<IMapperConfigurationExpression> mapperConfig)
        {
            var infrastructureConfig = new InfrastructureConfiguration();
            infrastructureConfig.ConfigureServices(services, configuration, env);

            // Register service
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mapperConfig(mc);
                mc.AddProfile(new MappingProfile());
                mc.CreateMap<IdentityRole, Role>().ReverseMap();
                mc.CreateMap<Tags, Tag>().ReverseMap();
                mc.CreateMap<Categories, Category>().ReverseMap();
                mc.CreateMap<Blogs, Blog>().ReverseMap();
                mc.CreateMap<BlogUser, UserInfo>().ReverseMap();
                mc.CreateMap<User, UserInfo>().ReverseMap();
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
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBlogService, BlogService>();

            services.AddTransient<IPasswordHasher<BlogUser>, PasswordHasher<BlogUser>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var infrastructureConfig = new InfrastructureConfiguration();
            infrastructureConfig.Configure(app, env);
        }
    }
}
