using BlogNetCore.Attributes;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewBlogs)]
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IViewModelFactory _viewModelFactory;

        public CategoryController(ICategoryService categoryService, IViewModelFactory viewModelFactory)
        {
            _categoryService = categoryService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var ownerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.SupervisorId).Value;
            var model = _categoryService.GetGroupCategories(x => x.OwnerId == ownerId);
            return View(model);
        }

        public IActionResult Detail(Guid? id)
        {
            var model = _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(id);
            return View(model);
        }

        public IActionResult CreateOrUpdate(Category category)
        {
            if (_categoryService.IsExisted(category.Id, category.Name, category.ParentId ?? Guid.Empty))
            {
                ViewBag.Error = "This category name is already exist";

                var model = category.Id == Guid.Empty ?
                    _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel()
                    : _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(category.Id);
                return View("Detail", model);
            }

            if (_categoryService.IsUrlExisted(category.Id, category.CategoryUrl, category.ParentId ?? Guid.Empty))
            {
                ViewBag.Error = "This category url is already exist";
                var model = category.Id == Guid.Empty ?
                    _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel()
                    : _viewModelFactory.GetService<ICategoryViewModelService>().CreateViewModel(category.Id);
                return View("Detail", model);
            }

            category.OwnerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.SupervisorId).Value;
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