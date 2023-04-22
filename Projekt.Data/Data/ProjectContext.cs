using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Sharded;
using Projekt.Data.Data.Shop;
using static System.Net.Mime.MediaTypeNames;

namespace Projekt.Data.Data
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Site> Site { get; set; }
        public DbSet<FaqItem> FaqItem { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
    }
}
