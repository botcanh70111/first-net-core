using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BlogNetCore.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly ISiteConfigService _siteConfigService;
        public HomeController(ISiteConfigService siteConfigService,
            IUserManager userManager) : base(userManager)
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