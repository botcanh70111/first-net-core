using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public partial class SiteConfigs
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Value { get; set; }
        public int? Order { get; set; }
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
    }
}
