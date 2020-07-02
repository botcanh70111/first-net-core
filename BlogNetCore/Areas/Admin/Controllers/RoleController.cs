using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using static Services.Constants.Constants;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewRoles, userType: UserTypes.Admin)]
    public class RoleController : BaseAdminController
    {
        private readonly IRoleService _roleService;
        private readonly IViewModelFactory _viewModelFactory;
        public RoleController(IRoleService roleService, 
            IViewModelFactory viewModelFactory,
            IUserManager userManager) : base(userManager)
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
            var model = _viewModelFactory.GetService<IRoleViewModelService>().CreateViewModel(null, roleId);
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