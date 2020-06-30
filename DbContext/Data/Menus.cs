using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public partial class Menus
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string UrlLink { get; set; }
        public Guid? ParentId { get; set; }
        public int? Order { get; set; }
        public bool? Active { get; set; }
        [StringLength(450)]
        public string OwnerId { get; set; }
    }
}
