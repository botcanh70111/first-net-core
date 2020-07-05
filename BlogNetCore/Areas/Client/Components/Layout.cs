using BlogNetCore.Client.Models.Login;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Client.Components
{
    [ViewComponent(Name = "Layout")]
    public class Layout : ViewComponent
    {
        private readonly ICookieService _cookieService;
        private readonly IViewModelFactory _viewModelFactory;

        public Layout(ICookieService cookieService, IViewModelFactory viewModelFactory)
        {
            _cookieService = cookieService;
            _viewModelFactory = viewModelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName)
        {
            var viewModelService = _viewModelFactory.GetService<ILayoutViewModelService>();
            switch(viewName)
            {
                case "Logo":
                    return View(viewName, viewModelService.CreateViewModelByType(_cookieService.BloggerId, Constants.SiteConfigTypes.Logo).LogoConfig);
                case "Footer":
                    return View(viewName, viewModelService.CreateViewModelByType(_cookieService.BloggerId, Constants.SiteConfigTypes.Footer).FooterConfigs);
                case "LoginModal":
                    return View(viewName, new LoginModel());
                case "RegisterModal":
                    return View(viewName, new RegisterModel());
                default:
                    return View(viewName, viewModelService.CreateViewModel(_cookieService.BloggerId));
            }
        }
    }
}
