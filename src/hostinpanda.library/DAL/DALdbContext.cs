﻿using System;
using System.Linq;

using hostinpanda.library.DAL.Tables;

using Microsoft.EntityFrameworkCore;

namespace hostinpanda.library.DAL
{
    public class DALdbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<Hosts> Hosts { get; set; }

        public DbSet<HostCheckLog> HostLog { get; set; }

        private readonly string _connectionString;

        public DALdbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DALdbContext(string databaseConnectionString)
        {
            _connectionString = databaseConnectionString;
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