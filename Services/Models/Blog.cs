using System;

namespace Services.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string BlogUrl { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public string PostScript { get; set; }
        public DateTime Edited { get; set; }
        public string EditedBy { get; set; }
        public long Views { get; set; }
    }
}
