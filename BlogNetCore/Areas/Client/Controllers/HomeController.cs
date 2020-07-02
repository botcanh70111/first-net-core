using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BlogNetCore.Client.Models;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class HomeController : BaseClientController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewModelFactory _viewModelFactory;

        public HomeController(ILogger<HomeController> logger, 
            IViewModelFactory viewModelFactory,
            IUserManager userManager,
            ICookieService cookieService) : base(userManager, cookieService)
        {
            _logger = logger;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index(string bloggerId = null)
        {
            if (bloggerId != null)
            {
                _cookieService.Set(CookieKeys.BloggerIdKey, bloggerId);
            }
            else
            {
                bloggerId = _cookieService.BloggerId;
            }

            var viewModel = _viewModelFactory.GetService<IHomeViewModelService>().CreateViewModel(bloggerId);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
