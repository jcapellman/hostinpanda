using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace hostinpanda.web.DAL
{
    public class DALdbContextMigration : IDesignTimeDbContextFactory<DALdbContext>
    {
        public DALdbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DALdbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=XXX;user id=XXX;password=XXX");

            return new DALdbContext(optionsBuilder.Options);
        }
    }
}