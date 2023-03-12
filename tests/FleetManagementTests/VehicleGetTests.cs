using FleetManagement.Models.Entities;
using FleetManagement.Models.Enums;
using FleetManagement.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using FleetManagementContext = FleetManagement.Configuration.FleetManagementContext;

namespace FleetManagementTests
{
    public class VehicleGetTests
    {
        private readonly Mock<FleetManagementContext> _context;

        public VehicleGetTests()
        {
            _context = new Mock<FleetManagementContext>(new DbContextOptions<FleetManagementContext>());
        }

        [Fact]
        public void GetAll_ShouldGet()
        {
            var chassis1 = new Chassis
            {
                ChassisId = "Chassis1",
                Series = "Series1",
                ChassisNumber = 1
            };
            var chassis2 = new Chassis
            {
                ChassisId = "Chassis2",
                Series = "Series2",
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
                },
                new Vehicle
                {
                    Id = 6,
                    Chassis = chassis2,
                    VehicleType = VehicleType.Truck,
                    Passengers = 1,
                    Color = "Blue"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var getResult = vehicleService.GetVehicles();

            Assert.NotNull(getResult);
            Assert.Equal(2, getResult.Count);
            Assert.Equal(5, getResult[0].Id);
            Assert.Equal(6, getResult[1].Id);
        }

        [Fact]
        public void GetOne_ShouldGet()
        {
            var chassis1 = new Chassis
            {
                ChassisId = "Chassis1",
                Series = "Series1",
                ChassisNumber = 1
            };
            var chassis2 = new Chassis
            {
                ChassisId = "Chassis2",
                Series = "Series2",
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
                },
                new Vehicle
                {
                    Id = 6,
                    Chassis = chassis2,
                    VehicleType = VehicleType.Truck,
                    Passengers = 1,
                    Color = "Blue"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var getResult = vehicleService.GetVehicleByChassisId("Chassis1");

            Assert.NotNull(getResult);
            Assert.Equal(5, getResult.Id);
            Assert.Equal("Chassis1", getResult.ChassisId);
        }

        [Fact]
        public void GetOne_NotFound_ShouldError()
        {
            var chassis1 = new Chassis
            {
                ChassisId = "Chassis1",
                Series = "Series1",
                ChassisNumber = 1
            };
            var chassis2 = new Chassis
            {
                ChassisId = "Chassis2",
                Series = "Series2",
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
                },
                new Vehicle
                {
                    Id = 6,
                    Chassis = chassis2,
                    VehicleType = VehicleType.Truck,
                    Passengers = 1,
                    Color = "Blue"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var exception = Assert.Throws<Exception>(() => vehicleService.GetVehicleByChassisId("Chassis3"));

            Assert.Equal("Vehicle Chassis not found", exception.Message);
        }
    }
}