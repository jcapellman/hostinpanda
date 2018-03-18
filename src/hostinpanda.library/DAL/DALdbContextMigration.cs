using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace hostinpanda.library.DAL
{
    public class DALdbContextMigration : IDesignTimeDbContextFactory<DALdbContext>
    {
        public DALdbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DALdbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=hostinpanda;user id=sa;password=hostinpanda");

            return new DALdbContext(optionsBuilder.Options);
        }
    }
}