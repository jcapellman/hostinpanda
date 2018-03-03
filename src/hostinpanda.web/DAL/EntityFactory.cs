using System;
using System.Linq;
using hostinpanda.web.DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace hostinpanda.web.DAL
{
    public class EntityFactory : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Hosts> Hosts { get; set; }

        private readonly string _connectionString;

        public EntityFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();

            if (changeSet == null)
            {
                return base.SaveChanges();
            }

            foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Member("Created").CurrentValue = DateTimeOffset.Now;
                        entry.Member("Active").CurrentValue = true;
                        break;
                    case EntityState.Deleted:
                        entry.Member("Active").CurrentValue = false;
                        break;
                }

                entry.Member("Modified").CurrentValue = DateTimeOffset.Now;
            }

            return base.SaveChanges();
        }
    }
}