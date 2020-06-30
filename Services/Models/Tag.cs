using System;

namespace Services.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TagUrl { get; set; }
        public string OwnerId { get; set; }
    }
}
