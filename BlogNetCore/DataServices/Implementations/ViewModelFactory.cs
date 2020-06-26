using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using System;

namespace BlogNetCore.DataServices.Implementations
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory(Lazy<IHomeViewModelService> homeViewModelService, Lazy<IRoleViewModelService> roleViewModelService, Lazy<IUserViewModelService> userViewModelService)
        {
            HomeViewModelService = homeViewModelService;
            RoleViewModelService = roleViewModelService;
            UserViewModelService = userViewModelService;
        }

        // Client services
        public Lazy<IHomeViewModelService> HomeViewModelService { get; set; }

        // Admin services
        public Lazy<IRoleViewModelService> RoleViewModelService { get; set; }
        public Lazy<IUserViewModelService> UserViewModelService { get; set; }
    }
}
