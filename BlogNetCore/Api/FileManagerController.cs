using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogNetCore.Api
{
    [UserAuthorizeAttributes(true)]
    public class FileManagerController : Controller
    {
        private readonly IFileHandler _fileHandler;
        private readonly ICookieService _cookieService;
        private readonly IUserManager _userManager;

        public FileManagerController(IFileHandler fileHandler, ICookieService cookieService, IUserManager userManager)
        {
            _fileHandler = fileHandler;
            _cookieService = cookieService;
            _userManager = userManager;
        }

        public IActionResult GetBlogFiles()
        {
            var supervisorId = _userManager.SupervisorId;
            var blogImagePath = new List<string> { "images", "blogs", supervisorId };
            var files = _fileHandler.GetFiles(blogImagePath);
            return Json(new { File = files });
        }
    }
}