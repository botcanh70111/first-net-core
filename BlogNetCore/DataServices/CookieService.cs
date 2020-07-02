using Microsoft.AspNetCore.Http;
using System;

namespace BlogNetCore.DataServices
{
    public static class CookieKeys
    {
        public const string BloggerIdKey = "BloggerIdKey";
    }

    public interface ICookieService
    {
        string BloggerId { get; }
        string Get(string key);
        void Set(string key, string value, int? expireMinute = null);
        string Update(string key, string value, int? expireMinute = null);
        void Remove(string key);
    }

    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string BloggerId => Get(CookieKeys.BloggerIdKey);

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public string Get(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public void Set(string key, string value, int? expireMinute = null)
        {
            CookieOptions option = new CookieOptions();

            if (expireMinute.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireMinute.Value);
            else
                option.Expires = DateTime.Now.AddDays(30);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        public string Update(string key, string value, int? expireMinute = null)
        {
            Remove(key);
            Set(key, value, expireMinute);
            return Get(key);
        }
    }
}
