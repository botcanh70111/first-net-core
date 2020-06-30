using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string CategoryUrl { get; set; }
        public Guid? ParentId { get; set; }
        public string OwnerId { get; set; }

        public IEnumerable<Category> ChildCategories { get; set; }
    }
}
