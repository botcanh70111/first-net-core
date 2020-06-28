using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewRoles)]
    public class RoleController : BaseAdminController
    {
        private readonly IRoleService _roleService;
        private readonly IViewModelFactory _viewModelFactory;
        public RoleController(IRoleService roleService, IViewModelFactory viewModelFactory)
        {
            _roleService = roleService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var model = _roleService.GetAllRoleClaims();
            return View(model);
        }

        public IActionResult Detail(string roleId)
        {
            var model = _viewModelFactory.RoleViewModelService.Value.CreateViewModel(roleId);
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [UserAuthorizeAttributes(claims: PermissionClaims.CreateRoles + "," + PermissionClaims.EditRoles)]
        public IActionResult CreateOrUpdate(RoleFormModel form)
        {
            _roleService.CreateOrUpdate(form.Role, form.AssignedClaims);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [UserAuthorizeAttributes(claims: PermissionClaims.DeleteRoles)]
        public IActionResult Delete(string id)
        {
            _roleService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}