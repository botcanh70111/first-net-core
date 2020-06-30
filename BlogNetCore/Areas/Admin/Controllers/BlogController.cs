using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.ViewBlogs)]
    public class BlogController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}