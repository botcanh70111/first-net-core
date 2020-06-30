using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public class BlogsTags
    {
        [Key]
        public Guid TagId { get; set; }
        [Key]
        public Guid BlogId { get; set; }
    }
}
