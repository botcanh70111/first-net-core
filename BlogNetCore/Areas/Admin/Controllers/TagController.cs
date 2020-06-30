using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;
using System.Linq;
using System.Security.Claims;
using static Services.Constants.Constants;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.CreateBlog + "," + PermissionClaims.EditBlog)]
    public class TagController : BaseAdminController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var ownerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.SupervisorId).Value;
            var tags = _tagService.GetAll(x => x.OwnerId == ownerId);
            return View(tags);
        }

        public IActionResult Detail(Guid? id)
        {
            Tag tag;
            if (id == null)
            {
                tag = new Tag();
            }
            else
            {
                tag = _tagService.GetById(id);
            }

            return View(tag);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateOrUpdate(Tag tag)
        {
            if (_tagService.IsExisted(tag.Name))
            {
                ViewBag.Error = "This tag name is already exist";
                return View("Detail", tag);
            }

            if ( _tagService.IsUrlExisted(tag.TagUrl))
            {
                ViewBag.Error = "This tag url is already exist";
                return View("Detail", tag);
            }

            if (tag.Id == null || tag.Id == Guid.Empty)
            {
                tag.OwnerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.SupervisorId).Value;
                _tagService.Create(tag);
            }
            else
            {
                _tagService.Update(tag);
            }

            return Redirect("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Guid id)
        {
            _tagService.Delete(id);

            return Redirect("Index");
        }
    }
}
