using FleetManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Configuration
{
    public class AppContext : DbContext
    {
        public readonly string _connectionString;
        public AppContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Chassis> Chasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
