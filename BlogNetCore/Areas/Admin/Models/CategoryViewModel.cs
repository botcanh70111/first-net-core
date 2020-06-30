using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
    }
}
