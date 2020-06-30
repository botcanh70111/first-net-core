using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public class Tags
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string TagUrl { get; set; }

        [StringLength(450)]
        public string OwnerId { get; set; }
    }
}
