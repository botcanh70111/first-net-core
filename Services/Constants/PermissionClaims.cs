using System.Collections.Generic;
using System.Security.Claims;

namespace Services.Constants
{
    public static class PermissionClaims
    {
        public const string EditConfigs = "EditConfigs";
        public const string CreateUser = "CreateUser";
        public const string EditUser = "EditUser";
        public const string DeleteUser = "DeleteUser";
        public const string AccessAdminMode = "AccessAdminMode";
        public const string CreateBlog = "CreateBlog";
        public const string EditBlog = "EditBlog";
        public const string DeleteBlog = "DeleteBlog";

        public static IEnumerable<string> AllRoleClaims()
        {
            return new List<string>
            {
                "EditConfigs",
                "CreateUser",
                "EditUser",
                "AccessAdminMode",
                "CreateBlog",
                "EditBlog",
                "DeleteBlog",
            };
        }
    }
}
