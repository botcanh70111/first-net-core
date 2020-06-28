using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public Menu Menu { get; set; }
        public IEnumerable<Menu> AllMenus { get; set; }
    }
}
