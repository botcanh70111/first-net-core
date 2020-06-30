using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Data
{
    public class BlogUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; } 
        public string Avatar { get; set; } 
        public string SupervisorId { get; set; } 
        public string UserType { get; set; } 
    }
}