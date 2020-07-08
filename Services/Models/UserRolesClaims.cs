using System.Collections.Generic;

namespace Services.Models
{
    public class UserRolesClaims
    {
        public User User { get; set; }

        // Users have the same supervisor Id
        public IEnumerable<string> GroupEmails { get; set; }
        public IEnumerable<RoleClaims> Roles { get; set; }
        public IEnumerable<UserClaim> UserClaims { get; set; }
        public string Error { get; set; }
    }

    public class UserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

    public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
