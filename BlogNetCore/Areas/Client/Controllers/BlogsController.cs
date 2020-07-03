using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BlogNetCore.Areas.Client.Controllers
{
    public class BlogsController : BaseClientController
    {
        private readonly IBlogService _blogService;

        public BlogsController(IUserManager userManager, 
            ICookieService cookieService,
            IBlogService blogService) : base(userManager, cookieService)
        {
            _blogService = blogService;
        }

        public IActionResult Index(string slug)
        {
            var bloggerId = _cookieService.BloggerId;
            var model = _blogService.GetBlogBySlug(slug, bloggerId);
            return View(model);
        }
    }
}