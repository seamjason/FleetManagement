using FleetManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Configuration
{
    public class FleetManagementContext : DbContext, IFleetManagementContext
    {
        public FleetManagementContext(DbContextOptions<FleetManagementContext> options) : base(options) { }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Chassis> Chasses { get; set; }

    }
}
