using Infrastructure.Data;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class MenuService : BaseService<Menus, Menu>, IMenuService
    {
        public MenuService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }

        public override bool Delete(object id, bool forceSave = true)
        {
            var children = _context.Menus.Where(x => x.ParentId == (Guid)id);
            if (children.Any())
            {
                foreach (var x in children) x.ParentId = null;
                _context.Menus.UpdateRange(children);
            }
            return base.Delete(id, forceSave);
        }

        public IEnumerable<Menu> GetGroupMenus(bool onlyActive = true, Expression<Func<Menus, bool>> predicate = null)
        {
            if (predicate == null) predicate = x => true;
            var menus = _context.Menus
                .Where(x => !onlyActive || (x.Active ?? false))
                .Where(predicate)
                .OrderBy(x => x.Order);
            var menuModels = _mapper.Map<IEnumerable<Menu>>(menus);
            return GroupMenus(menuModels);
        }

        private IEnumerable<Menu> GroupMenus(IEnumerable<Menu> menus, Guid? parentId = null)
        {
            var rootLevel = menus.Where(x => (parentId == null && !x.ParentId.HasValue) || x.ParentId == parentId);
            foreach (var m in rootLevel)
            {
                m.ChildMenus = GroupMenus(menus, m.Id);
            }

            return rootLevel;
        }
    }
}
