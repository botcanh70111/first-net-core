using BlogNetCore.Areas.Admin.Models.FormModels;
using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using BlogNetCore.DataServices.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorize(claims: PermissionClaims.ViewBlogs)]
    public class BlogController : BaseAdminController
    {
        private readonly IBlogService _blogService;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IModelMapper _mapper;
        private readonly IFileHandler _fileHandler;
        private readonly ICookieService _cookieService;

        public BlogController(IBlogService blogService, 
            IUserManager userManager, 
            IViewModelFactory viewModelFactory,
            IModelMapper mapper,
            IFileHandler fileHandler,
            ICookieService cookieService) : base(userManager)
        {
            _blogService = blogService;
            _viewModelFactory = viewModelFactory;
            _mapper = mapper;
            _fileHandler = fileHandler;
            _cookieService = cookieService;
        }

        public IActionResult Index()
        {
            var bloggerId = _userManager.SupervisorId;
            var blogs = _blogService.GetBlogsByBloggerId(bloggerId);
            return View(blogs);
        }

        public IActionResult Detail(Guid id)
        {
            var blog = _viewModelFactory.GetService<IBlogViewModelService>().CreateViewModel(_userManager.SupervisorId, id);
            return View(blog);
        }

        [HttpPost]
        [UserAuthorize(claims: PermissionClaims.CreateBlog)]
        public IActionResult CreateOrUpdate(BlogFormModel blog)
        {
            if (_blogService.IsUrlExisted(blog.BlogUrl, blog.Id, _userManager.SupervisorId))
            {
                ViewBag.Error = "The url is already exist";
                var viewModel = _viewModelFactory.GetService<IBlogViewModelService>().CreateViewModel(_userManager.SupervisorId, blog.Id);
                _mapper.Map<BlogFormModel, Blog>(blog, viewModel.Blog);
                return View("Detail", viewModel);
            }

            var filePath = blog.ImageUrl;
            
            if (blog.Image != null && blog.Image.Length > 0)
            {
                var fileName = _fileHandler.SaveFile(blog.Image, new List<string> { "images", "blogs", _userManager.SupervisorId });
                filePath = fileName;
            }

            if (blog.Id == Guid.Empty)
            {
                var newBlog = new Blog();
                _mapper.Map<BlogFormModel, Blog>(blog, newBlog);
                newBlog.ImageUrl = filePath;
                newBlog.Created = DateTime.UtcNow;
                newBlog.Edited = DateTime.UtcNow;
                newBlog.CreatedBy = _userManager.UserId;
                newBlog.EditedBy = _userManager.UserId;
                newBlog.BloggerId = _userManager.SupervisorId;
                _blogService.Create(newBlog);
            }
            else
            {
                var updateBlog = _blogService.GetById(blog.Id);
                _mapper.Map<BlogFormModel, Blog>(blog, updateBlog);
                updateBlog.ImageUrl = filePath;
                updateBlog.Edited = DateTime.UtcNow;
                updateBlog.EditedBy = _userManager.UserId;

                _blogService.Update(updateBlog);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [UserAuthorize(claims: PermissionClaims.DeleteBlog)]
        public IActionResult Delete(Guid id)
        {
            _blogService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult ViewBlog(string slug)
        {
            _cookieService.Set(CookieKeys.BloggerIdKey, _userManager.SupervisorId);
            return Redirect("/blogs/" + slug);
        }
    }
}