using Infrastructure.Data;
using Services.Models;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ISiteConfigService : IAppService<SiteConfigs,SiteConfig>
    {
        IEnumerable<SiteConfig> GetConfigsByType(string type);
    }
}
