using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [UserAuthorize(claims: PermissionClaims.AccessBloggerMode)] 
    public abstract class BaseAdminController : Controller
    {
        protected readonly IUserManager _userManager;

        protected BaseAdminController(IUserManager userManager)
        {
            _userManager = userManager;
        }
    }
}