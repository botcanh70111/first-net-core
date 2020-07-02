using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class BlogModel
    {
        public BlogModel()
        {
            Blog = new Blog();
        }

        public Blog Blog { get; set; } 

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
