using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;

namespace BlogNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttributes : ActionFilterAttribute
    {
        private readonly bool _isAuthorize;
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

        public UserAuthorizeAttributes(string claimType, string claims)
        {
            _claims = claims;
            _claimType = claimType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_isAuthorize)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                }
            }

            if (!string.IsNullOrEmpty(_claims) && string.IsNullOrEmpty(_claimType))
            {
                var claims = _claims.Split(',').Select(x => x.Trim()).ToList();
                var userClaims = context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                if (!claims.Intersect(userClaims).Any())
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                } 
            }

            if (!string.IsNullOrEmpty(_claims) && !string.IsNullOrEmpty(_claimType))
            {
                var claims = _claims.Split(',').ToList();
                var userClaims = context.HttpContext.User.Claims;
                foreach(var c in claims)
                {
                    if (!userClaims.Where(x => x.Type == _claimType && x.Value == c.Trim()).Any())
                    {
                        context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                    }
                }
            }
        }
    }
}
