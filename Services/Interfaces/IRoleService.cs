using Microsoft.AspNetCore.Identity;
using Services.Models;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IRoleService : IAppService<IdentityRole, Role>
    {
        bool IsExisted(string roleName);
        Role CreateOrUpdate(Role role, IEnumerable<string> claims);
        RoleClaims GetRoleClaims(string roleId);
        IEnumerable<RoleClaims> GetAllRoleClaims();
    }
}
