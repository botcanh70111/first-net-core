using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class BlogsController : BaseClientController
    {
        public BlogsController(IUserManager userManager, ICookieService cookieService) : base(userManager, cookieService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}