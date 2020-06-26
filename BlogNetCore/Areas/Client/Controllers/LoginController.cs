using BlogNetCore.Client.Models.Login;
using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class LoginController : BaseClientController
    {
        private readonly IUserManager _userManager;
        private readonly IUserService _userService;

        public LoginController(IUserManager userManager, 
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginForm)
        {
            var profile = _userService.GetProfile(loginForm.Email, loginForm.Password);
            if (string.IsNullOrEmpty(profile.Error))
            {
                _userManager.SignIn(HttpContext, profile, loginForm.RememberMe);
                return Redirect(loginForm.ReturnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _userManager.SignOut(HttpContext);
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            await _userManager.CreateAsync(HttpContext, register);
            return Redirect(register.ReturnUrl);
        }
    }
}
