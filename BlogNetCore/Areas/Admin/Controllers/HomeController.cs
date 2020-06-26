using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: "AccessAdminMode")]
    public class HomeController : BaseAdminController
    {
        private readonly ISiteConfigService _siteConfigService;
        public HomeController(ISiteConfigService siteConfigService)
        {
            _siteConfigService = siteConfigService;
        }

        public IActionResult Index()
        {
            var allConfigs = _siteConfigService.GetAll();
            return View();
        }
    }
}