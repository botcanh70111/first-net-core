using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.EditConfigs)]
    public class SiteConfigController : BaseAdminController
    {
        private IFileHandler _fileHandler;
        private ISiteConfigService _siteConfigService;

        public SiteConfigController(IFileHandler fileHandler, 
            ISiteConfigService siteConfigService,
            IUserManager userManager) : base(userManager)
        {
            _fileHandler = fileHandler;
            _siteConfigService = siteConfigService;
        }

        public IActionResult Logo()
        {
            var ownerId = _userManager.SupervisorId;
            var model = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Logo, x => x.OwnerId == ownerId).FirstOrDefault();
            if (model == null) model = new SiteConfig();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLogo(LogoModel model)
        {
            var filePath = model.Value;
            var logo = new SiteConfig
            {
                Id = model.Id,
                Name = model.Name,
                Type = Constants.SiteConfigTypes.Logo,
                Value = filePath
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                var fileName = _fileHandler.SaveFile(model.Image, new List<string> { "images", "logos" });
                logo.Value = fileName;
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

        public IActionResult Index(string type)
        {
            var ownerId = _userManager.SupervisorId;
            var model = _siteConfigService.GetConfigsByType(type, x => x.OwnerId == ownerId);
            if (model == null) model = new List<SiteConfig>();
            ViewData["Type"] = type;
            return View(model);
        }

        public IActionResult Detail(Guid? id, string type)
        {
            SiteConfig model;
            if (id != null)
            {
                model = _siteConfigService.GetById(id);
            }
            else
            {
                model = new SiteConfig();
            }
            ViewData["Type"] = type;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrUpdate(SiteConfig config)
        {
            config.OwnerId = _userManager.SupervisorId; ;
            if (config.Id == null || config.Id == Guid.Empty)
                _siteConfigService.Create(config);
            else
            {
                _siteConfigService.Update(config);
            }
            return RedirectToAction("Index", new { type = config.Type });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, string type)
        {
            var model = _siteConfigService.Delete(id);
            return RedirectToAction("Index", new { type = type });
        }
    }
}