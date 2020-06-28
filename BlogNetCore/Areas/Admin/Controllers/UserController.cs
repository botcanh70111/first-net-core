using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewUsers)]
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IUserManager _userManager;
        private readonly IViewModelFactory _viewModelFactory;

        public UserController(IUserService userService, IUserManager userManager, IViewModelFactory viewModelFactory)
        {
            _userService = userService;
            _userManager = userManager;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll();
            return View(users);
        }

        public IActionResult Detail(string userId)
        {
            var viewModel = _viewModelFactory.UserViewModelService.Value.CreateViewModel(userId);
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(UserFormModel form)
        {
            _userService.Update(form.User, form.Roles, form.UserClaims);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateUser(UserFormModel form)
        {
            if (_userManager.Validate(form.User.Email, form.User.UserName, form.Password, form.ConfirmPassword))
            {
                await _userService.RegisterUserWithPermission(form.User, form.Password, form.Roles, form.UserClaims);
            }

            return RedirectToAction("Index");
        }
    }
}