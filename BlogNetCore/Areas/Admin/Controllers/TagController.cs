using BlogNetCore.Attributes;
using BlogNetCore.DataServices;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [UserAuthorizeAttributes(claims: PermissionClaims.CreateBlog + "," + PermissionClaims.EditBlog)]
    public class TagController : BaseAdminController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService, IUserManager userManager) : base(userManager)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var ownerId = _userManager.SupervisorId;
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
            if (_tagService.IsExisted(tag.Name, tag.Id, _userManager.SupervisorId))
            {
                ViewBag.Error = "This tag name is already exist";
                return View("Detail", tag);
            }

            if ( _tagService.IsUrlExisted(tag.TagUrl, tag.Id, _userManager.SupervisorId))
            {
                ViewBag.Error = "This tag url is already exist";
                return View("Detail", tag);
            }

            if (tag.Id == null || tag.Id == Guid.Empty)
            {
                tag.OwnerId = _userManager.SupervisorId;
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
