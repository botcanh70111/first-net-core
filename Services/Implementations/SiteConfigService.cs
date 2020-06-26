using Infrastructure.Data;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations
{
    public class SiteConfigService : BaseService<SiteConfigs, SiteConfig>, ISiteConfigService
    {
        public SiteConfigService(ApplicationDbContext context,
            IModelMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<SiteConfig> GetConfigsByType(string type)
        {
            var configs = _context.SiteConfigs.Where(x => x.Type == type).OrderBy(x => x.Order);
            return _mapper.Map<IEnumerable<SiteConfig>>(configs);
        }
    }
}
