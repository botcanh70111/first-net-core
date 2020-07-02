using Infrastructure.Data;
using Services.Extensions;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class CategoryService : BaseService<Categories, Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }

        public override Category Create(Category model, bool forceSave = true)
        {
            MakeCategoryUrl(model);
            return base.Create(model, forceSave);
        }

        public override Category Update(Category model, bool forceSave = true)
        {
            MakeCategoryUrl(model);
            return base.Update(model, forceSave);
        }

        public IEnumerable<Category> GetGroupCategories(Expression<Func<Categories, bool>> predicate = null)
        {
            if (predicate == null) predicate = x => true;
            var categories = _context.Categories
                .Where(predicate);

            var categoryModels = _mapper.Map<IEnumerable<Category>>(categories);
            return GroupCategories(categoryModels);
        }

        private IEnumerable<Category> GroupCategories(IEnumerable<Category> categories, Guid? parentId = null)
        {
            var rootLevel = categories.Where(x => (parentId == null && !x.ParentId.HasValue) || x.ParentId == parentId);
            foreach (var c in rootLevel)
            {
                c.ChildCategories = GroupCategories(categories, c.Id);
            }

            return rootLevel;
        }

        public bool IsExisted(Guid id, string name, Guid parentId, string ownerId)
        {
            return _context.Categories.FirstOrDefault(x => x.Name == name && x.Id != id && x.OwnerId == ownerId
            && ((parentId == Guid.Empty && x.ParentId == null ) || x.ParentId == parentId)) != null;
        }

        public bool IsUrlExisted(Guid id, string url, Guid parentId, string ownerId)
        {
            return _context.Categories.FirstOrDefault(x => x.CategoryUrl == url && x.Id != id && x.OwnerId == ownerId
            && ((parentId == Guid.Empty && x.ParentId == null) || x.ParentId == parentId)) != null;
        }

        public override bool Delete(object id, bool forceSave = true)
        {
            var children = _context.Categories.Where(x => x.ParentId == (Guid)id);
            if (children.Any())
            {
                foreach (var x in children) x.ParentId = null;
                _context.Categories.UpdateRange(children);
            }

            return base.Delete(id, forceSave);
        }

        private void MakeCategoryUrl(Category model)
        {
            if (string.IsNullOrEmpty(model.CategoryUrl))
                model.CategoryUrl = model.Name.ToAliasUrl();
            else
                model.CategoryUrl = model.CategoryUrl.ToAliasUrl();

            if (IsUrlExisted(model.Id, model.CategoryUrl, model.ParentId ?? Guid.Empty, model.OwnerId))
                model.CategoryUrl = model.CategoryUrl + "-" + DateTime.Now.Ticks;
        }

        public IEnumerable<Category> GetGroupCategoriesByOwnerId(string ownerId, Expression<Func<Categories, bool>> predicate = null)
        {
            if (predicate == null) predicate = x => true;
            
            var categories = _context.Categories
                .Where(x => x.OwnerId == ownerId)
                .Where(predicate);

            var categoryModels = _mapper.Map<IEnumerable<Category>>(categories);
            return GroupCategories(categoryModels);
        }
    }
}
