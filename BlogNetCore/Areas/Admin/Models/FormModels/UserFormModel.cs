using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models.FormModels
{
    public class UserFormModel
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
