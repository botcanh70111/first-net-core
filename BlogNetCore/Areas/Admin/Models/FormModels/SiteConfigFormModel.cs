using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNetCore.Areas.Admin.Models.FormModels
{
    public class SiteConfigFormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
    }
}
