using BlogNetCore.Client.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNetCore.DataServices
{
    public interface IUserManager
    {
        bool HasPermission(HttpContext httpContext, string permission);
        void SignIn(HttpContext httpContext, UserRolesClaims profile, bool isPersistent);
        Task CreateAsync(HttpContext httpContext, RegisterModel register);
        void SignOut(HttpContext httpContext);
        bool Validate(string email, string userName, string password, string confirmPassword);
    }

    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public bool HasPermission(HttpContext httpContext, string permission)
        {
            var claims = httpContext.User.Claims;
            if (claims.Where(x => x.Value == permission).Any())
                return true;

            return false;
        }

        public bool Validate(string email, string userName, string password, string confirmPassword)
        {
            return true;
        }

        public async void SignIn(HttpContext httpContext, UserRolesClaims profile, bool isPersistent)
        {
            ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(profile), 
                CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                principal, 
                new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = isPersistent 
                });
        }

        public async Task CreateAsync(HttpContext httpContext, RegisterModel register)
        {
            if (Validate(register.Email, register.UserName, register.Password, register.ConfirmPassword))
            {
                var newUser = new User();
                newUser.Email = register.Email;
                newUser.UserName = register.UserName;
                newUser.FirstName = register.FirstName;
                newUser.LastName = register.LastName;
                var user = await _userService.RegisterUser(newUser, register.Password);
                if (user != null)
                {
                    SignIn(httpContext, user, true);
                }
            }
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> GetUserClaims(UserRolesClaims user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(BlogClaimTypes.Id, user.User.Id.ToString()));
            claims.Add(new Claim(BlogClaimTypes.SupervisorId, string.IsNullOrEmpty(user.User.SupervisorId) ? user.User.Id.ToString() : user.User.SupervisorId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.User.FirstName + " " + user.User.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.User.Email));
            claims.Add(new Claim(ClaimTypes.System, user.User.UserType ?? ""));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.User.UserName));

            claims.AddRange(GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(UserRolesClaims user)
        {
            List<Claim> claims = new List<Claim>();
            foreach(var c in user.UserClaims)
            {
                claims.Add(new Claim(c.ClaimType, c.ClaimValue));
            }

            foreach (var r in user.Roles)
            {
                foreach (var c in r.Claims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, c));
                }
            }

            return claims;
        }
    }
}
