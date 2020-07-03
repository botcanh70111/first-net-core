using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using System;

namespace BlogNetCore.DataServices.Implementations
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory(Lazy<ILayoutViewModelService> layoutViewModelService, Lazy<IHomeViewModelService> homeViewModelService, Lazy<IRoleViewModelService> roleViewModelService, Lazy<IUserViewModelService> userViewModelService, Lazy<IMenuViewModelService> menuViewModelService, Lazy<ICategoryViewModelService> categoryViewModelService, Lazy<IBlogViewModelService> blogViewModelService)
        {
            LayoutViewModelService = layoutViewModelService;
            HomeViewModelService = homeViewModelService;
            RoleViewModelService = roleViewModelService;
            UserViewModelService = userViewModelService;
            MenuViewModelService = menuViewModelService;
            CategoryViewModelService = categoryViewModelService;
            BlogViewModelService = blogViewModelService;
        }

        // Client services
        private Lazy<ILayoutViewModelService> LayoutViewModelService { get; set; }
        private Lazy<IHomeViewModelService> HomeViewModelService { get; set; }

        // Admin services
        private Lazy<IRoleViewModelService> RoleViewModelService { get; set; }
        private Lazy<IUserViewModelService> UserViewModelService { get; set; }
        private Lazy<IMenuViewModelService> MenuViewModelService { get; set; }
        private Lazy<ICategoryViewModelService> CategoryViewModelService { get; set; }
        private Lazy<IBlogViewModelService> BlogViewModelService { get; set; }

        public TService GetService<TService>()
        {
            if (typeof(TService) == typeof(ILayoutViewModelService)) return (TService)LayoutViewModelService.Value;
            if (typeof(TService) == typeof(IHomeViewModelService)) return (TService)HomeViewModelService.Value;
            if (typeof(TService) == typeof(IRoleViewModelService)) return (TService)RoleViewModelService.Value;
            if (typeof(TService) == typeof(IUserViewModelService)) return (TService)UserViewModelService.Value;
            if (typeof(TService) == typeof(IMenuViewModelService)) return (TService)MenuViewModelService.Value;
            if (typeof(TService) == typeof(ICategoryViewModelService)) return (TService)CategoryViewModelService.Value;
            if (typeof(TService) == typeof(IBlogViewModelService)) return (TService)BlogViewModelService.Value;

            return default;
        }
    }
}
