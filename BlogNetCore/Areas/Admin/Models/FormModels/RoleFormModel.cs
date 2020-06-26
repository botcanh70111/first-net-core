using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models.FormModels
{
    public class RoleFormModel
    {
        public Role Role { get; set; }
        public IList<string> AssignedClaims { get; set; }
    }
}
