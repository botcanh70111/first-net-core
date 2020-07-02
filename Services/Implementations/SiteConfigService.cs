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
    public class SiteConfigService : BaseService<SiteConfigs, SiteConfig>, ISiteConfigService
    {
        public SiteConfigService(ApplicationDbContext context,
            IModelMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<SiteConfig> GetConfigsByType(string type, Expression<Func<SiteConfigs, bool>> predicate = null)
        {
            if (predicate == null) predicate = x => true;
            var configs = _context.SiteConfigs.Where(x => x.Type == type).Where(predicate).OrderBy(x => x.Order);
            return _mapper.Map<IEnumerable<SiteConfig>>(configs);
        }

        public IEnumerable<SiteConfig> GetConfigsByTypeAndOwnerId(string type, string ownerId)
        {
            var configs = _context.SiteConfigs.Where(x => x.Type == type && x.OwnerId == ownerId).OrderBy(x => x.Order);
            return _mapper.Map<IEnumerable<SiteConfig>>(configs);
        }
    }
}
