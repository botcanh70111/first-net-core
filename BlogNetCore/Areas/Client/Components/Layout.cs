using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;
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
            var viewModel = _viewModelFactory.GetService<ILayoutViewModelService>().CreateViewModel(_cookieService.BloggerId);
            switch(viewName)
            {
                case "Logo":
                    return View(viewName, viewModel.LogoConfig);
                case "Footer":
                    return View(viewName, viewModel.FooterConfigs);
                case "LoginModal":
                    return View(viewName, viewModel.Login);
                case "RegisterModal":
                    return View(viewName, viewModel.RegisterForm);
                default:
                    return View(viewName, viewModel);
            }
        }
    }
}
