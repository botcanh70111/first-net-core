using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public SiteConfig SiteConfig { get; set; }
        public User User { get; set; }
    }
}
