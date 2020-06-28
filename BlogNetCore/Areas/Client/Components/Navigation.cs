using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Client.Components
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation : ViewComponent
    {
        private readonly IMenuService _menuService;

        public Navigation(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = _menuService.GetGroupMenus();
            return View(menu);
        }
    }
}
