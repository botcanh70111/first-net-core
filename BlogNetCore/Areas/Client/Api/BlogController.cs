using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BlogNetCore.Areas.Client.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ICookieService _cookieService;

        public BlogController(IBlogService blogService, ICookieService cookieService)
        {
            _blogService = blogService;
            _cookieService = cookieService;
        }

        public JsonResult Get(string slug, string bloggerId)
        {
            if (string.IsNullOrEmpty(bloggerId))
            {
                bloggerId = _cookieService.BloggerId;
            }
            
            var model = _blogService.GetBlogBySlug(slug, bloggerId);
            return new JsonResult(model);
        }

        [HttpGet("list")]
        public JsonResult List(string bloggerId)
        {
            if (string.IsNullOrEmpty(bloggerId))
            {
                bloggerId = _cookieService.BloggerId;
            }

            var model = _blogService.GetBlogsByBloggerId(bloggerId);
            return new JsonResult(model);
        }
    }
}