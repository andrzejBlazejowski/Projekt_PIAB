using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Shop;

namespace Projekt.Data.Data
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Site> Site { get; set; }
        public DbSet<FaqItem> FaqItem { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
