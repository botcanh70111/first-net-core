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

        public SideBarComponent(ISiteConfigService siteConfigService)
        {
            _siteConfigService = siteConfigService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName = null)
        {
            if (viewName == "Logo")
            {
                var model = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Logo).FirstOrDefault();
                return View(viewName, model);
            }

            if (string.IsNullOrEmpty(viewName)) return View();

            return View(viewName);
        }
    }
}
