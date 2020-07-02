using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models.FormModels
{
    public class BlogFormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string BlogUrl { get; set; }
        public string ImageUrl { get; set; }
        public string PostScript { get; set; }
        public DateTime Edited { get; set; }
        public string EditedBy { get; set; }
        public Guid? CategoryId { get; set; }
        public IEnumerable<Guid> BlogTagIds { get; set; }
        public IFormFile Image { get; set; }
    }
}
