using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Constants;
using System;
using System.Linq;
using System.Security.Claims;
using static Services.Constants.Constants;

namespace BlogNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttributes : ActionFilterAttribute
    {
        private readonly bool _isAuthorize;
        private readonly string _userType;
        private readonly string _claims;
        private readonly string _claimType;

        public UserAuthorizeAttributes(bool isAuthorize = true)
        {
            _isAuthorize = isAuthorize;
        }

        public UserAuthorizeAttributes(string claims)
        {
            _claims = claims;
        }

        public UserAuthorizeAttributes(string claimType = "", string claims = "", string userType = "", bool isAuthorize = false)
        {
            _claims = claims;
            _claimType = claimType;
            _userType = userType;
            _isAuthorize = isAuthorize;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userClaims = context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            if (userClaims.Contains(PermissionClaims.FullAdminRight))
            {
                return;
            }

            if (_isAuthorize)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                }
            }

            if (!string.IsNullOrEmpty(_userType))
            {
                var userType = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.UserType)?.Value;
                if (userType == _userType || userType == UserTypes.Admin)
                {
                    return;
                } 
                else
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_claims) && string.IsNullOrEmpty(_claimType))
                {
                    var claims = _claims.Split(',').Select(x => x.Trim()).ToList();
                    if (!claims.Intersect(userClaims).Any())
                    {
                        context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                    }
                }

                if (!string.IsNullOrEmpty(_claims) && !string.IsNullOrEmpty(_claimType))
                {
                    var claims = _claims.Split(',').ToList();
                    var userClaimsTypes = context.HttpContext.User.Claims;
                    foreach (var c in claims)
                    {
                        if (!userClaimsTypes.Where(x => x.Type == _claimType && x.Value == c.Trim()).Any())
                        {
                            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                        }
                    }
                }
            }
        }
    }
}
