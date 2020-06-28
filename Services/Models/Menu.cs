using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlLink { get; set; }
        public Guid? ParentId { get; set; }
        public int? Order { get; set; }
        public bool? Active { get; set; }

        public IEnumerable<Menu> ChildMenus { get; set; }
    }
}
