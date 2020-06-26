using BlogNetCore.Client.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNetCore.DataServices
{
    public interface IUserManager
    {
        void SignIn(HttpContext httpContext, UserRolesClaims profile, bool isPersistent);
        Task CreateAsync(HttpContext httpContext, RegisterModel register);
        void SignOut(HttpContext httpContext);
    }
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
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

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> GetUserClaims(UserRolesClaims user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.User.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.User.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.User.Email));
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
