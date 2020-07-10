using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Client.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ICookieService _cookieService;

        public LayoutController(IMenuService menuService,
            ICookieService cookieService)
        {
            _menuService = menuService;
            _cookieService = cookieService;
        }

        public JsonResult Navigation(string bloggerId)
        {
            IEnumerable<Menu> menu;
            if (string.IsNullOrEmpty(bloggerId))
            {
                menu = _menuService.GetGroupMenusByOwnerId(_cookieService.BloggerId);
            }
            else
            {
                menu = _menuService.GetGroupMenusByOwnerId(bloggerId);
            }

            return new JsonResult(menu);
        }
    }
}