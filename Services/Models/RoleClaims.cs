using System.Collections.Generic;

namespace Services.Models
{
    public class RoleClaims
    {
        public Role Role { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
