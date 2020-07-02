using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data
{
    public class Blogs
    {
        [Key]
        public Guid Id {get;set;}
        [MaxLength(256)]
        [Required]
        public string Name {get;set;}

        [MaxLength(1000)]
        public string Description {get;set;}
        public string Content {get;set;}

        [MaxLength(256)]
        [Required]
        public string BlogUrl {get;set;}
        public Guid? CategoryId {get;set;}
        [Required]
        public DateTime Created {get;set;}
        [MaxLength(450)]
        [Required]
        public string CreatedBy {get;set;}

        [MaxLength(256)]
        public string ImageUrl { get; set; }
        [MaxLength(1000)]
        public string PostScript { get; set; }
        [Required]
        public DateTime Edited { get; set; }
        [MaxLength(450)]
        public string EditedBy { get; set; }
        [Required]
        public long Views { get; set; }
        [MaxLength(450)]
        [Required]
        public string BloggerId { get; set; }
    }
}
