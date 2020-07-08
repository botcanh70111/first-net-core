using BlogNetCore.Client.Models.Login;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        void ExternalSignIn(HttpContext httpContext, ExternalLoginInfo info);
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task CreateAsync(HttpContext httpContext, RegisterModel register);
        void SignOut(HttpContext httpContext);
        bool Validate(string email, string userName, string password, string confirmPassword);
        string SupervisorId { get; }
        string UserId { get; }
        string UserName { get; }
        string FullName { get; }
        string Email { get; }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<BlogUser> _signInManager;
        public string SupervisorId => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.SupervisorId)?.Value;
        public string UserId => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.Id)?.Value;
        public string UserType => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.UserType)?.Value;
        public string UserName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        public string FullName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        public string Email => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        public UserManager(IUserService userService, IHttpContextAccessor httpContextAccessor, SignInManager<BlogUser> signInManager)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
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
        public async void ExternalSignIn(HttpContext httpContext, ExternalLoginInfo info)
        {
            ClaimsIdentity identity = new ClaimsIdentity(info.Principal.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = false
                });
        }

        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
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

            claims.Add(new Claim(BlogClaimTypes.UserType, user.User.UserType ?? ""));
            claims.Add(new Claim(BlogClaimTypes.Id, user.User.Id.ToString()));
            claims.Add(new Claim(BlogClaimTypes.SupervisorId, 
                string.IsNullOrEmpty(user.User.SupervisorId) ? user.User.Id.ToString() : user.User.SupervisorId.ToString()));
            claims.Add(new Claim(BlogClaimTypes.GroupEmails, string.Join(",", user.GroupEmails)));
            claims.Add(new Claim(BlogClaimTypes.FullName, $"{user.User.FirstName} {user.User.LastName}"));

            claims.Add(new Claim(ClaimTypes.Name, user.User.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.User.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.User.Email));

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
