using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;

namespace BlogNetCore.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        private readonly IMenuService _menuService;
        private readonly IViewModelFactory _viewModelFactory;

        public MenuController(IMenuService menuService, IViewModelFactory viewModelFactory)
        {
            _menuService = menuService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var menus = _menuService.GetGroupMenus(false);
            return View(menus);
        }

        public IActionResult Detail(Guid? id = null)
        {
            var viewModel = _viewModelFactory.MenuViewModelService.Value.CreateViewModel(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Menu menu)
        {
            _menuService.Create(menu);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Menu menu)
        {
            _menuService.Update(menu);
            return RedirectToAction("Index");
        }
    }
}