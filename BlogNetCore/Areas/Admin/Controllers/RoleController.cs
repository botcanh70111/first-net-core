using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BlogNetCore.Areas.Admin.Controllers
{
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
        public IActionResult CreateOrUpdate(RoleFormModel form)
        {
            _roleService.CreateOrUpdate(form.Role, form.AssignedClaims);

            return RedirectToAction("Index");
        }
    }
}