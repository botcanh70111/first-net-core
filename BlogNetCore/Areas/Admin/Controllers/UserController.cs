using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorize(claims: PermissionClaims.ViewUsers)]
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IViewModelFactory _viewModelFactory;

        public UserController(IUserService userService, 
            IUserManager userManager, 
            IViewModelFactory viewModelFactory) : base(userManager)
        {
            _userService = userService;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var ownerId = _userManager.SupervisorId;
            var users = _userService.GetUsersBySupervisorId(ownerId);
            return View(users);
        }

        public IActionResult Detail(string userId)
        {
            var viewModel = _viewModelFactory.GetService<IUserViewModelService>().CreateViewModel(null, userId);
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [UserAuthorize(claims: PermissionClaims.EditUser)]
        public IActionResult Update(UserFormModel form)
        {
            _userService.Update(form.User, form.Roles, form.UserClaims);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [UserAuthorize(claims: PermissionClaims.CreateUser)]
        public async Task<IActionResult> CreateUser(UserFormModel form)
        {
            if (_userManager.Validate(form.User.Email, form.User.UserName, form.Password, form.ConfirmPassword))
            {
                var supervisorId = _userManager.SupervisorId;
                await _userService.RegisterUserWithPermission(form.User, form.Password, form.Roles, form.UserClaims, supervisorId);
            }

            return RedirectToAction("Index");
        }
    }
}