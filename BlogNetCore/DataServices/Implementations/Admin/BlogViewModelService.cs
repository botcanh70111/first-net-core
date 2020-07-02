using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.DataServices.Interfaces.Admin;
using Services.Interfaces;
using System;

namespace BlogNetCore.DataServices.Implementations.Admin
{
    public class BlogViewModelService : IBlogViewModelService
    {
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public BlogViewModelService(IBlogService blogService, ITagService tagService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public BlogModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new BlogModel();
            viewModel.Tags = _tagService.GetTagsByOwnerId(ownerId);
            viewModel.Categories = _categoryService.GetGroupCategoriesByOwnerId(ownerId);
            if (contentKey == null || (Guid)contentKey == Guid.Empty) return viewModel;

            viewModel.Blog = _blogService.GetById(contentKey);
            return viewModel;
        }
    }
}
