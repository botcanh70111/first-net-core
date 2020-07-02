using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Client.Controllers
{
    [Area("Client")]
    public abstract class BaseClientController : Controller
    {
        protected readonly IUserManager _userManager;
        protected readonly ICookieService _cookieService;

        protected BaseClientController(IUserManager userManager, ICookieService cookieService)
        {
            _userManager = userManager;
            _cookieService = cookieService;
        }
    }
}