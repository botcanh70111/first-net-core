using BlogNetCore.Client.Models.Login;
using Services.Models;
using System.Collections.Generic;

namespace BlogNetCore.Client.Models
{
    public class LayoutViewModel
    {
        public string Title { get; set; }
        public IEnumerable<SiteConfig> FooterConfigs { get; set; }
        public IEnumerable<SiteConfig> SocialConfigs { get; set; }
        public SiteConfig LogoConfig { get; set; }
        public IEnumerable<Menu> Menus { get; set; }

        // Login form
        public LoginModel Login { get; set; }
        public RegisterModel RegisterForm { get; set; }

        public LayoutViewModel()
        {
            Title = "Tan's Blog";
            LogoConfig = new SiteConfig();
            SocialConfigs = new List<SiteConfig>();
            FooterConfigs = new List<SiteConfig>();
            Menus = new List<Menu>();
            Login = new LoginModel();
        }
    }
}
