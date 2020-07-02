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
                role.Id = role.Name.ToLower() + System.DateTime.Now.Ticks;
                var newRole = Create(role, false);
                
                if (claims != null)
                {
                    var newClaims = new List<IdentityRoleClaim<string>>();
                    foreach (var c in claims)
                    {
                        newClaims.Add(new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = c, RoleId = newRole.Id });
                    }
                    _context.RoleClaims.AddRange(newClaims);
                }
                
                _context.SaveChanges();
                return newRole;
            } 
            else
            {
                var roleEntity = _context.Roles.FirstOrDefault(r => r.Id == role.Id);
                if (roleEntity.Name != role.Name && !IsExisted(role.Name))
                {
                    roleEntity.Name = role.Name;
                } 

                roleEntity.NormalizedName = role.NormalizedName;
                roleEntity.ConcurrencyStamp = role.ConcurrencyStamp;
                var updatedRole = _context.Roles.Update(roleEntity);
                var oldClaims = _context.RoleClaims.Where(x => x.RoleId == updatedRole.Entity.Id);
                _context.RoleClaims.RemoveRange(oldClaims);

                if (claims != null)
                {
                    var newClaims = new List<IdentityRoleClaim<string>>();
                    foreach (var c in claims)
                    {
                        newClaims.Add(new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = c, RoleId = updatedRole.Entity.Id });
                    }

                    _context.RoleClaims.AddRange(newClaims);
                }

                _context.SaveChanges();
                return _mapper.Map<Role>(updatedRole.Entity);
            }
        }

        public RoleClaims GetRoleClaims(string roleId)
        {
            var roleClaims = (from role in _context.Roles
                              join claim in _context.RoleClaims on role.Id equals claim.RoleId into RoleClaims
                              from rc in RoleClaims.DefaultIfEmpty()
                              select new
                              {
                                  Role = role,
                                  RoleClaims = rc
                              }).ToLookup(x => x.Role)
                             .Select(x => new { Role = x.Key, Claims = x.Select(x => x.RoleClaims).Where(c => c != null).ToList() })
                             .FirstOrDefault(x => x.Role.Id == roleId);

            var model = new RoleClaims();
            model.Role = _mapper.Map<Role>(roleClaims.Role);
            model.Claims = roleClaims.Claims != null ? roleClaims.Claims.Select(x => x.ClaimValue) : new List<string>();

            return model;
        }

        public IEnumerable<RoleClaims> GetAllRoleClaims()
        {
            var roleClaims = (from role in _context.Roles
                              join claim in _context.RoleClaims on role.Id equals claim.RoleId into RoleClaims
                              from rc in RoleClaims.DefaultIfEmpty()
                              select new { Role = role, RoleClaims = rc }
                             ).ToLookup(x => x.Role)
                             .Select(x => new { Role = x.Key, Claims = x.Select(c => c.RoleClaims).Where(c => c != null).ToList() });

            var convertedRoleClaims = roleClaims.Select(x => 
                new RoleClaims { Role = _mapper.Map<Role>(x.Role), Claims = x.Claims != null ? x.Claims.Select(c => c.ClaimValue) : new List<string>() });

            return convertedRoleClaims;
        }
    }
}
