using System.Collections.Generic;

namespace Services.Models
{
    public class BlogInfo
    {
        public Blog Blog { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public UserInfo Author { get; set; }
    }
}
