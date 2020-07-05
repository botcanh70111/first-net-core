using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Admin.Components
{
    [ViewComponent(Name = "SideBar")]
    public class SideBarComponent : ViewComponent
    {
        private readonly ISiteConfigService _siteConfigService;
        private readonly IUserManager  _userManager;

        public SideBarComponent(ISiteConfigService siteConfigService, IUserManager userManager)
        {
            _siteConfigService = siteConfigService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName = null)
        {
            if (viewName == "Logo")
            {
                var model = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Logo, _userManager.SupervisorId).FirstOrDefault();
                return View(viewName, model);
            }

            if (string.IsNullOrEmpty(viewName)) return View();

            return View(viewName);
        }
    }
}
