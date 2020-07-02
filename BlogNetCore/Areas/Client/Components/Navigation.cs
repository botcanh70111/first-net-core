using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Client.Components
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation : ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly ICookieService _cookieService;

        public Navigation(IMenuService menuService,
            ICookieService cookieService)
        {
            _menuService = menuService;
            _cookieService = cookieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = _menuService.GetGroupMenusByOwnerId(_cookieService.BloggerId);
            return View(menu);
        }
    }
}
