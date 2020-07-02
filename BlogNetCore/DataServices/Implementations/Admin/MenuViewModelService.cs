using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.DataServices.Interfaces.Admin;
using Services.Interfaces;
using System;

namespace BlogNetCore.DataServices.Implementations.Admin
{
    public class MenuViewModelService : IMenuViewModelService
    {
        private readonly IMenuService _menuService;

        public MenuViewModelService(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public MenuViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new MenuViewModel();

            if (contentKey != null)
            {
                var model = _menuService.GetById(contentKey);
                viewModel.Menu = model;
            }
            else
            {
                viewModel.Menu = new Services.Models.Menu();
            }

            var allMenu = _menuService.GetGroupMenusByOwnerId(ownerId, false, x => (contentKey == null || x.Id != (Guid)contentKey));
            viewModel.AllMenus = allMenu;

            return viewModel;
        }
    }
}
