using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BlogNetCore.Client.Models;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class HomeController : BaseClientController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
            IViewModelFactory viewModelFactory,
            IUserService userService,
            IUserManager userManager,
            ICookieService cookieService) : base(userManager, cookieService)
        {
            _logger = logger;
            _viewModelFactory = viewModelFactory;
            _userService = userService;
        }

        public IActionResult Index(string bloggerId = null)
        {
            if (bloggerId != null)
            {
                Response.Cookies.Append(CookieKeys.BloggerIdKey, bloggerId);
                _cookieService.Set(CookieKeys.BloggerIdKey, bloggerId);
                return RedirectToAction("Index");
            }
            else
            {
                var b = Request.Cookies[CookieKeys.BloggerIdKey];
                bloggerId = _cookieService.BloggerId;
            }

            if (bloggerId == null)
            {
                var model = _userService.GetBloggers();
                return View("_BloggerSelect", model);
            }

            var viewModel = _viewModelFactory.GetService<IHomeViewModelService>().CreateViewModel(bloggerId);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        } 
        
        public IActionResult SelectBlogSite()
        {
            _cookieService.Remove(CookieKeys.BloggerIdKey);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
