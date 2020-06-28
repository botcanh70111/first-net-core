﻿using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using System;

namespace BlogNetCore.DataServices.Implementations
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory(Lazy<IHomeViewModelService> homeViewModelService, Lazy<IRoleViewModelService> roleViewModelService, Lazy<IUserViewModelService> userViewModelService, Lazy<IMenuViewModelService> menuViewModelService)
        {
            HomeViewModelService = homeViewModelService;
            RoleViewModelService = roleViewModelService;
            UserViewModelService = userViewModelService;
            MenuViewModelService = menuViewModelService;
        }

        // Client services
        public Lazy<IHomeViewModelService> HomeViewModelService { get; set; }

        // Admin services
        public Lazy<IRoleViewModelService> RoleViewModelService { get; set; }
        public Lazy<IUserViewModelService> UserViewModelService { get; set; }
        public Lazy<IMenuViewModelService> MenuViewModelService { get; set; }

        public TService GetService<TService>()
        {
            if (typeof(TService) == typeof(IHomeViewModelService)) return (TService)HomeViewModelService.Value;
            if (typeof(TService) == typeof(IRoleViewModelService)) return (TService)RoleViewModelService.Value;
            if (typeof(TService) == typeof(IUserViewModelService)) return (TService)UserViewModelService.Value;
            if (typeof(TService) == typeof(IMenuViewModelService)) return (TService)MenuViewModelService.Value;

            return default;
        }
    }
}
