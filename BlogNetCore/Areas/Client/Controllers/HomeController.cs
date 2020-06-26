using System.Diagnostics;
using BlogNetCore.Client.Models;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class HomeController : BaseClientController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewModelFactory _viewModelFactory;

        public HomeController(ILogger<HomeController> logger, IViewModelFactory viewModelFactory)
        {
            _logger = logger;
            _viewModelFactory = viewModelFactory;
        }

        public IActionResult Index()
        {
            var viewModel = _viewModelFactory.HomeViewModelService.Value.CreateViewModel();
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
