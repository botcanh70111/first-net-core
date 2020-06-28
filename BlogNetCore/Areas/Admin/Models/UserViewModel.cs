using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class UserViewModel : AdminViewModel
    {
        public UserViewModel()
        {
            User = new User();
            Roles = new List<Role>();
            UserClaims = new List<UserClaim>();
        }

        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserClaim> UserClaims { get; set; }
        public IEnumerable<string> AllRoleClaims { get; set; }
        public IEnumerable<Role> AllRoles { get; set; }
    }
}
