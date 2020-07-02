using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.DataServices.Interfaces.Admin;
using Services.Interfaces;
using System;

namespace BlogNetCore.DataServices.Implementations.Admin
{
    public class CategoryViewModelService : ICategoryViewModelService
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewModelService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public CategoryViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var model = new CategoryViewModel();
            
            if (contentKey != null)
            {
                model.Category = _categoryService.GetById(contentKey);
            }
            else
            {
                model.Category = new Services.Models.Category();
            }
            
            model.AllCategories = _categoryService.GetGroupCategoriesByOwnerId(ownerId, x => (contentKey == null || x.Id != (Guid)contentKey));
            return model;
        }
    }
}
