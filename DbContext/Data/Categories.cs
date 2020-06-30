using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public class Categories
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        public string Content { get; set; }
        [Required]
        [StringLength(256)]
        public string CategoryUrl { get; set; }

        public Guid? ParentId { get; set; }
        [StringLength(450)]
        public string OwnerId { get; set; }
    }
}
