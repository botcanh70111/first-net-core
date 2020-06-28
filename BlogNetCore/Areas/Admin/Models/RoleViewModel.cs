using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class RoleViewModel : AdminViewModel
    {
        public Role Role { get; set; }
        public IEnumerable<string> AssignedClaims { get; set; }
        public IEnumerable<string> AllRoleClaims { get; set; }
    }
}
