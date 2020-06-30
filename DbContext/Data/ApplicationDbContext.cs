using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<SiteConfigs> SiteConfigs { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<BlogsTags> BlogsTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogsTags>()
                .HasKey(c => new { c.BlogId, c.TagId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
