using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Constants;
using Services.Interfaces;
using Services.Mappers;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Services.Constants.Constants;

namespace Services.Implementations
{
    public class UserService : BaseService<BlogUser, User>, IUserService
    {
        private readonly IPasswordHasher<BlogUser> _passwordHasher;
        private readonly UserManager<BlogUser> _identityUserManager;
        public UserService(ApplicationDbContext context, 
            IModelMapper mapper,
            IPasswordHasher<BlogUser> passwordHasher,
            UserManager<BlogUser> identityUserManager) : base(context, mapper)
        {
            _passwordHasher = passwordHasher;
            _identityUserManager = identityUserManager;
        }

        public void Update(User user, IEnumerable<string> roles, IEnumerable<string> userClaims)
        {
            var entityUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            entityUser.PhoneNumber = user.PhoneNumber;
            entityUser.FirstName = user.FirstName;
            entityUser.LastName = user.LastName;
            entityUser.Avatar = user.Avatar;
            entityUser.BirthDay = user.BirthDay;

            _context.Users.Update(entityUser);

            _context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == user.Id));
            _context.UserClaims.RemoveRange(_context.UserClaims.Where(x => x.UserId == user.Id && x.ClaimType == ClaimTypes.Role));

            AddRoleClaimForUser(user.Id, roles, userClaims);

            _context.SaveChanges();
        }

        public UserRolesClaims GetProfile(string email, string password)
        {
            var profile = new UserRolesClaims();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            if (user != null)
            {
                var verifyPass = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if (verifyPass == PasswordVerificationResult.Success)
                {
                    profile = GetProfile(user);
                }
                else
                {
                    profile.Error = "Password was wrong";
                }

                return profile;
            }

            profile.Error = "The email is not exist";
            return profile;
        }

        public UserRolesClaims GetProfile(string userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return GetProfile(user);
            }

            return null;
        }

        private UserRolesClaims GetProfile(IdentityUser user)
        {
            var profile = new UserRolesClaims();
            var userRoles = _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId);
            var roles = (from role in _context.Roles
                         where userRoles.Contains(role.Id)
                         join claim in _context.RoleClaims on role.Id equals claim.RoleId into RoleClaims
                         from rc in RoleClaims.DefaultIfEmpty()
                         select new
                         {
                             Role = role,
                             Claims = rc
                         }).ToLookup(x => x.Role)
                         .Select(x => new { Role = x.Key, Claims = x.Select(c => c.Claims).Where(c => c != null).ToList() });
                
            var userClaims = _context.UserClaims.Where(x => x.UserId == user.Id);
            var roleClaims = new List<RoleClaims>();
            foreach (var r in roles)
            {
                var roleClaim = new RoleClaims();
                roleClaim.Role = _mapper.Map<Role>(r.Role);
                roleClaim.Claims = r.Claims.Select(x => x.ClaimValue);
                roleClaims.Add(roleClaim);
            }

            profile.User = _mapper.Map<User>(user);
            profile.UserClaims = _mapper.Map<IEnumerable<UserClaim>>(userClaims);
            profile.Roles = roleClaims;

            return profile;
        }

        public async Task<UserRolesClaims> RegisterUser(User user, string password)
        {
            var newUser = new BlogUser();
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;

            var result = await _identityUserManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                return GetProfile(newUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserRolesClaims> RegisterUserWithPermission(User user, string password, IEnumerable<string> roles, IEnumerable<string> userClaims, string supervisorId)
        {
            var newUser = new BlogUser();
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.UserType = UserTypes.BlogEditor;
            newUser.SupervisorId = supervisorId;

            var result = await _identityUserManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                AddRoleClaimForUser(newUser.Id, roles, userClaims);

                _context.SaveChanges();

                return GetProfile(newUser);
            }
            else
            {
                return null;
            }
        }

        private void AddRoleClaimForUser(string userId, IEnumerable<string> roles, IEnumerable<string> userClaims)
        {
            if (roles != null && roles.Count() > 0)
            {
                var rolesList = new List<IdentityUserRole<string>>();
                foreach (var r in roles)
                {
                    rolesList.Add(new IdentityUserRole<string> { UserId = userId, RoleId = r });
                }

                _context.UserRoles.AddRange(rolesList);
            }

            if (userClaims != null && userClaims.Count() > 0)
            {
                var userClaimsList = new List<IdentityUserClaim<string>>();
                foreach (var c in userClaims)
                {
                    userClaimsList.Add(new IdentityUserClaim<string> { UserId = userId, ClaimType = ClaimTypes.Role, ClaimValue = c });
                }

                _context.UserClaims.AddRange(userClaimsList);
            }
        }

        public IEnumerable<User> GetUsersBySupervisorId(string supervisorId)
        {
            var users = _context.Users.Where(x => x.SupervisorId == supervisorId);
            return _mapper.Map<IEnumerable<User>>(users);
        }
    }
}
