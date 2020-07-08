using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Client.Components
{
    [ViewComponent(Name = "OnlineBlock")]
    [UserAuthorize(true)]
    public class OnlineBlock : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IUserManager _userManager;

        public OnlineBlock(IUserService userService, IUserManager userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = _userService.GetAll(x => x.Email != _userManager.Email);
            return View(users);
        }
    }
}
