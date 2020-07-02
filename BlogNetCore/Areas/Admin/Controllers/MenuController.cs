using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;
using System.Linq;

namespace BlogNetCore.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        private readonly IMenuService _menuService;
        private readonly IViewModelFactory _viewModelFactory;

        public MenuController(IMenuService menuService, 
            IViewModelFactory viewModelFactory,
            IUserManager userManager) : base(userManager)
        {
            _menuService = menuService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var ownerId = _userManager.SupervisorId;
            var menus = _menuService.GetGroupMenus(false, x => x.OwnerId == ownerId);
            return View(menus);
        }

        public IActionResult Detail(Guid? id = null)
        {
            var ownerId = _userManager.SupervisorId;
            var viewModel = _viewModelFactory.GetService<IMenuViewModelService>().CreateViewModel(ownerId, id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Menu menu)
        {
            menu.OwnerId = _userManager.SupervisorId;
            _menuService.Create(menu);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Menu menu)
        {
            _menuService.Update(menu);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _menuService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}