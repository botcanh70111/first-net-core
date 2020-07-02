using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface ISiteConfigService : IAppService<SiteConfigs,SiteConfig>
    {
        IEnumerable<SiteConfig> GetConfigsByType(string type, Expression<Func<SiteConfigs, bool>> predicate = null);
        IEnumerable<SiteConfig> GetConfigsByTypeAndOwnerId(string type, string ownerId);
    }
}
