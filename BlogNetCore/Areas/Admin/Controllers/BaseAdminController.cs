using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [UserAuthorizeAttributes(claims: PermissionClaims.AccessAdminMode + "," + PermissionClaims.AccessBloggerMode)] 
    public abstract class BaseAdminController : Controller
    {
    }
}