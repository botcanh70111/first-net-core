using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Services.Implementations
{
    public class RoleService : BaseService<IdentityRole, Role>, IRoleService
    {
        public RoleService(ApplicationDbContext context, IModelMapper mapper) : base(context, mapper)
        {
        }

        public bool IsExisted(string roleName)
        {
            return _context.Roles.Where(x => x.Name == roleName).Any();
        }

        public Role CreateOrUpdate(Role role, IEnumerable<string> claims)
        {
            if (string.IsNullOrEmpty(role.Id) && !IsExisted(role.Name))
            {
                role.Id = role.Name.ToLower() + System.DateTime.Now.ToString();
                var newRole = Create(role, false);
                var newClaims = new List<IdentityRoleClaim<string>>();
                foreach(var c in claims)
                {
                    newClaims.Add(new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = c, RoleId = newRole.Id });
                }

                _context.RoleClaims.AddRange(newClaims);
                _context.SaveChanges();
                return newRole;
            } 
            else
            {
                var roleEntity = GetById(role.Id);
                if (roleEntity.Name != role.Name && !IsExisted(role.Name))
                {
                    roleEntity.Name = role.Name;
                } 

                roleEntity.NormalizedName = role.NormalizedName;
                roleEntity.ConcurrencyStamp = role.ConcurrencyStamp;
                var updatedRole = Update(roleEntity);
                var oldClaims = _context.RoleClaims.Where(x => x.RoleId == updatedRole.Id);
                _context.RoleClaims.RemoveRange(oldClaims);
                var newClaims = new List<IdentityRoleClaim<string>>();
                foreach (var c in claims)
                {
                    newClaims.Add(new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = c, RoleId = updatedRole.Id });
                }

                _context.RoleClaims.AddRange(newClaims);
                _context.SaveChanges();
                return updatedRole;
            }
        }

        public RoleClaims GetRoleClaims(string roleId)
        {
            var roleClaims = _context.Roles
                .Join(_context.RoleClaims.DefaultIfEmpty(), r => r.Id, c => c.RoleId, (r, c) => new { Role = r, Claims = c})
                .ToLookup(x => x.Role).Select(x => new { Role = x.Key, Claims = x.Select(c => c.Claims) })
                .FirstOrDefault(x => x.Role.Id == roleId);

            var model = new RoleClaims();
            model.Role = _mapper.Map<Role>(roleClaims.Role);
            model.Claims = roleClaims.Claims != null ? roleClaims.Claims.Select(x => x.ClaimValue) : new List<string>();

            return model;
        }

        public IEnumerable<RoleClaims> GetAllRoleClaims()
        {
            var roleClaims = _context.Roles
                .Join(_context.RoleClaims.DefaultIfEmpty(),
                    r => r.Id, c => c.RoleId, (r, c) => new { Role = r, Claims = c })
                .ToLookup(x => x.Role).Select(x => new { Role = x.Key, Claims = x.Select(c => c.Claims) });

            var convertedRoleClaims = roleClaims.Select(x => 
                new RoleClaims { Role = _mapper.Map<Role>(x.Role), Claims = x.Claims.Select(c => c.ClaimValue) });

            return convertedRoleClaims;
        }
    }
}
