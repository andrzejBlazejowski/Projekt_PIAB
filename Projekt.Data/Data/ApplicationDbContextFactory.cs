using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Projekt.Data.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ProjectContext>
    {
        public ProjectContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjectContext;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new ProjectContext(optionsBuilder.Options);
        }
    }
}
