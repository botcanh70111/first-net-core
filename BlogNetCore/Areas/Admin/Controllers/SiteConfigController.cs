using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.EditConfigs)]
    public class SiteConfigController : BaseAdminController
    {
        private IWebHostEnvironment _hostingEnvironment;
        private ISiteConfigService _siteConfigService;

        public SiteConfigController(IWebHostEnvironment hostingEnvironment, ISiteConfigService siteConfigService)
        {
            _hostingEnvironment = hostingEnvironment;
            _siteConfigService = siteConfigService;
        }

        public IActionResult Logo()
        {
            var model = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Logo).FirstOrDefault();
            if (model == null) model = new Services.Models.SiteConfig();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLogo(LogoModel model)
        {
            var filePath = model.Value;
            var logo = new Services.Models.SiteConfig
            {
                Id = model.Id,
                Name = model.Name,
                Type = Constants.SiteConfigTypes.Logo,
                Value = filePath
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.Image.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    model.Image.CopyTo(stream);
                }

                logo.Value = "images/" + model.Image.FileName;
            }

            if (model.Id == null || model.Id == Guid.Empty)
            {
                _siteConfigService.Create(logo);
            } 
            else
            {
                _siteConfigService.Update(logo);
            }

            return RedirectToAction("Logo");
        }

        public IActionResult Footer()
        {
            return View();
        }

        public IActionResult Social()
        {
            return View();
        }
    }
}