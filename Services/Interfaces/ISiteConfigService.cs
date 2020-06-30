using Infrastructure.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface ISiteConfigService : IAppService<SiteConfigs,SiteConfig>
    {
        IEnumerable<SiteConfig> GetConfigsByType(string type, Expression<Func<SiteConfigs, bool>> predicate = null);
    }
}
