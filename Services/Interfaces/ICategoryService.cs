using Infrastructure.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface ICategoryService : IAppService<Categories, Category>
    {
        bool IsExisted(Guid id, string name, Guid parentId, string ownerId);
        bool IsUrlExisted(Guid id, string url, Guid parentId, string ownerId);
        IEnumerable<Category> GetGroupCategories(Expression<Func<Categories, bool>> predicate = null);
        IEnumerable<Category> GetGroupCategoriesByOwnerId(string ownerId, Expression<Func<Categories, bool>> predicate = null);
    }
}
