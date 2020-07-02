using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewBlogs)]
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IViewModelFactory _viewModelFactory;

        public CategoryController(ICategoryService categoryService, 
            IViewModelFactory viewModelFactory,
            IUserManager userManager) : base(userManager)
        {
            _categoryService = categoryService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var ownerId = _userManager.SupervisorId;
            var model = _categoryService.GetGroupCategories(x => x.OwnerId == ownerId);
            return View(model);
        }

        public IActionResult Detail(Guid? id)
        {
            var ownerId = _userManager.SupervisorId;
            var model = _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(ownerId, id);
            return View(model);
        }

        public IActionResult CreateOrUpdate(Category category)
        {
            var ownerId = _userManager.SupervisorId;
            if (_categoryService.IsExisted(category.Id, category.Name, category.ParentId ?? Guid.Empty, _userManager.SupervisorId))
            {
                ViewBag.Error = "This category name is already exist";

                var model = category.Id == Guid.Empty ?
                    _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(ownerId)
                    : _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(ownerId, category.Id);
                return View("Detail", model);
            }

            if (_categoryService.IsUrlExisted(category.Id, category.CategoryUrl, category.ParentId ?? Guid.Empty, _userManager.SupervisorId))
            {
                ViewBag.Error = "This category url is already exist";
                var model = category.Id == Guid.Empty ?
                    _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(ownerId)
                    : _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(ownerId, category.Id);
                return View("Detail", model);
            }

            category.OwnerId = _userManager.SupervisorId;
            if (category.Id == null || category.Id == Guid.Empty)
            {
                _categoryService.Create(category);
            }
            else
            {
                _categoryService.Update(category);
            }

            return Redirect("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _categoryService.Delete(id);
            return Redirect("Index");
        }
    }
}