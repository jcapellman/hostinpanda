using hostinpanda.serverlibrary.DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace hostinpanda.serverlibrary.DAL
{
    public class BaseDAL : DbContext
    {
        public DbSet<Hosts> Hosts { get; set; }
    }
}