using FleetManagement.Models;
using FleetManagement.Models.Entities;
using FleetManagement.Models.Enums;
using FleetManagement.Services;
using Moq;
using Moq.EntityFrameworkCore;
using AppContext = FleetManagement.Configuration.AppContext;

namespace FleetManagementTests
{
    public class VehicleUpdateTests
    {
        private readonly Mock<AppContext> _context;

        public VehicleUpdateTests()
        {
            _context = new Mock<AppContext>("");
        }

        [Fact]
        public void ShouldUpdate()
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

            var vehicleRequest = new VehicleRequest
            {
                Id = 5,
                ChassisId = "Chassis2",
                ChassisNumber = 3,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Blue"
            };

            var updateResult = vehicleService.UpdateVehicle(vehicleRequest);

            Assert.NotNull(updateResult);
            Assert.Equal("Chassis1", updateResult.ChassisId);
            Assert.Equal(1, updateResult.ChassisNumber);
            Assert.Equal("Series", updateResult.ChassisSeries);
            Assert.Equal((int)VehicleType.Truck, updateResult.VehicleTypeId);
            Assert.Equal(1, updateResult.Passengers);
            Assert.Equal("Blue", updateResult.Color);
        }

        [Fact]
        public void DuplicateChassis_ShouldIgnoreAndUpdate()
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

            var vehicleRequest = new VehicleRequest
            {
                Id = 5,
                ChassisId = "Chassis2",
                ChassisNumber = 3,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Blue"
            };

            var updateResult = vehicleService.UpdateVehicle(vehicleRequest);

            Assert.NotNull(updateResult);
            Assert.Equal("Chassis1", updateResult.ChassisId);
            Assert.Equal(1, updateResult.ChassisNumber);
            Assert.Equal("Series", updateResult.ChassisSeries);
            Assert.Equal((int)VehicleType.Truck, updateResult.VehicleTypeId);
            Assert.Equal(1, updateResult.Passengers);
            Assert.Equal("Blue", updateResult.Color);
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

            var vehicleRequest = new VehicleRequest
            {
                Id = 6,
                ChassisId = "Chassis2",
                ChassisNumber = 3,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Blue"
            };

            var exception = Assert.Throws<Exception>(() => vehicleService.UpdateVehicle(vehicleRequest));

            Assert.Equal("Vehicle not found", exception.Message);
        }

    }
}