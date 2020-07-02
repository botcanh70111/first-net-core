using System.Collections.Generic;

namespace Services.Constants
{
    public static class PermissionClaims
    {
        public const string FullAdminRight = "FullAdminRight";
        public const string AccessAdminMode = "AccessAdminMode";

        public const string ViewRoles = "ViewRoles";
        public const string CreateRoles = "CreateRoles";
        public const string EditRoles = "EditRoles";
        public const string DeleteRoles = "DeleteRoles";

        public const string AccessBloggerMode = "AccessBloggerMode";

        public const string EditConfigs = "EditConfigs";

        public const string ViewUsers = "ViewUsers";
        public const string CreateUser = "CreateUser";
        public const string EditUser = "EditUser";
        public const string DeleteUser = "DeleteUser";

        public const string ViewBlogs = "ViewBlogs";
        public const string CreateBlog = "CreateBlog";
        public const string EditBlog = "EditBlog";
        public const string DeleteBlog = "DeleteBlog";

        public static IEnumerable<string> AllRoleClaims()
        {
            return new List<string>
            {
                "FullAdminRight",
                "AccessAdminMode",
                "AccessBloggerMode",
                "EditConfigs",

                "ViewUsers",
                "CreateUser",
                "EditUser",
                 "DeleteUser",

                "CreateBlog",
                "EditBlog",
                "DeleteBlog",
                "ViewBlogs",

                 "ViewRoles",
                 "CreateRoles",
                 "EditRoles",
                 "DeleteRoles",
            };
        }

        public static IEnumerable<string> AllRoleBloggerClaims()
        {
            return new List<string>
            {
                "AccessBloggerMode",
                "EditConfigs",

                "ViewUsers",
                "CreateUser",
                "EditUser",
                 "DeleteUser",

                "CreateBlog",
                "EditBlog",
                "DeleteBlog",
                "ViewBlogs",

                 "ViewRoles",
                 "CreateRoles",
                 "EditRoles",
                 "DeleteRoles",
            };
        }
    }
}
