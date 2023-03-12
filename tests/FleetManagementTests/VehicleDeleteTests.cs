using FleetManagement.Models.Entities;
using FleetManagement.Models.Enums;
using FleetManagement.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using FleetManagementContext = FleetManagement.Configuration.FleetManagementContext;

namespace FleetManagementTests
{
    public class VehicleDeleteTests
    {
        private readonly Mock<FleetManagementContext> _context;

        public VehicleDeleteTests()
        {
            _context = new Mock<FleetManagementContext>(new DbContextOptions<FleetManagementContext>());
        }

        [Fact]
        public void ShouldDelete()
        {
            var chassis1 = new Chassis
            {
                ChassisId = "Chassis1",
                Series = "Series",
                ChassisNumber = 1
            };
            var chassis2 = new Chassis
            {
                ChassisId = "Chassis2",
                Series = "Series",
                ChassisNumber = 2
            };

            var _testChasses = new List<Chassis>()
            {
                chassis1,
                chassis2
            }
            .AsQueryable();

            var _testVehicles = new List<Vehicle>()
            {
                new Vehicle
                {
                    Id = 5,
                    Chassis = chassis1,
                    VehicleType = VehicleType.Truck,
                    Passengers = 1,
                    Color = "Red"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var deleteResult = vehicleService.DeleteVehicle(5);

            Assert.True(deleteResult);
            _context.Verify(v => v.Vehicles.Remove(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public void NotFound_ShouldError()
        {
            var chassis = new Chassis
            {
                ChassisId = "Chassis1",
                Series = "Series",
                ChassisNumber = 1
            };

            var _testChasses = new List<Chassis>()
            {
                chassis
            }
            .AsQueryable();

            var _testVehicles = new List<Vehicle>()
            {
                new Vehicle
                {
                    Id = 5,
                    Chassis = chassis,
                    VehicleType = VehicleType.Truck,
                    Passengers = 1,
                    Color = "Red"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var exception = Assert.Throws<Exception>(() => vehicleService.DeleteVehicle(6));

            Assert.Equal("Vehicle not found", exception.Message);
        }

    }
}