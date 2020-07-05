using Microsoft.AspNetCore.Http;
using System;

namespace BlogNetCore.Areas.Admin.Models.FormModels
{
    public class LogoModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } 
        public string Value { get; set; } 
        public string OwnerId { get; set; } 
        public IFormFile Image { get; set; } 
    }
}
