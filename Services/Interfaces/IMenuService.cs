using Infrastructure.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface IMenuService : IAppService<Menus, Menu>
    {
        IEnumerable<Menu> GetGroupMenus(bool onlyActive = true, Expression<Func<Menus, bool>> predicate = null);
        IEnumerable<Menu> GetGroupMenusByOwnerId(string ownerId, bool onlyActive = true, Expression<Func<Menus, bool>> predicate = null);
    }
}
