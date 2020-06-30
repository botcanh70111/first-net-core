using System;

namespace Services.Models
{
    public class SiteConfig
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? Order { get; set; }
        public string Type { get; set; }
        public string OwnerId { get; set; }
    }
}
