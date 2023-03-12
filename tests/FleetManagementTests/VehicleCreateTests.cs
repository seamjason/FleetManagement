using FleetManagement.Models;
using FleetManagement.Models.Entities;
using FleetManagement.Models.Enums;
using FleetManagement.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using FleetManagementContext = FleetManagement.Configuration.FleetManagementContext;

namespace FleetManagementTests
{
    public class VehicleCreateTests
    {
        private readonly Mock<FleetManagementContext> _context;

        public VehicleCreateTests()
        {
            _context = new Mock<FleetManagementContext>(new DbContextOptions<FleetManagementContext>());
        }

        [Fact]
        public void Truck_ShouldCreate_Passengers1()
        {
            var testVehicles = new List<Vehicle>().AsQueryable();
            var testChasses = new List<Chassis>().AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "TestChassis",
                ChassisNumber = 1,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Truck,
                Color = "Red"
            };

            var addResult = vehicleService.CreateVehicle(vehicleRequest);

            Assert.NotNull(addResult);
            Assert.Equal("TestChassis", addResult.ChassisId);
            Assert.Equal(1, addResult.ChassisNumber);
            Assert.Equal("ChassisSeries", addResult.ChassisSeries);
            Assert.Equal(2, addResult.VehicleTypeId);
            Assert.Equal(1, addResult.Passengers);
            Assert.Equal("Red", addResult.Color);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public void Bus_ShouldCreate_Passengers42()
        {
            var testVehicles = new List<Vehicle>().AsQueryable();
            var testChasses = new List<Chassis>().AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "TestChassis",
                ChassisNumber = 1,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Red"
            };

            var addResult = vehicleService.CreateVehicle(vehicleRequest);

            Assert.NotNull(addResult);
            Assert.Equal("TestChassis", addResult.ChassisId);
            Assert.Equal(1, addResult.ChassisNumber);
            Assert.Equal("ChassisSeries", addResult.ChassisSeries);
            Assert.Equal(1, addResult.VehicleTypeId);
            Assert.Equal(42, addResult.Passengers);
            Assert.Equal("Red", addResult.Color);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public void Car_ShouldCreate_Passengers4()
        {
            var testVehicles = new List<Vehicle>().AsQueryable();
            var testChasses = new List<Chassis>().AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "TestChassis",
                ChassisNumber = 1,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Car,
                Color = "Red"
            };

            var addResult = vehicleService.CreateVehicle(vehicleRequest);

            Assert.NotNull(addResult);
            Assert.Equal("TestChassis", addResult.ChassisId);
            Assert.Equal(1, addResult.ChassisNumber);
            Assert.Equal("ChassisSeries", addResult.ChassisSeries);
            Assert.Equal(3, addResult.VehicleTypeId);
            Assert.Equal(4, addResult.Passengers);
            Assert.Equal("Red", addResult.Color);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public void ExistingVehicles_ShouldCreate()
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
                    Chassis = chassis,
                    VehicleType = VehicleType.Truck,
                    Color = "Red"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "Chassis2",
                ChassisNumber = 3,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Red"
            };

            var addResult = vehicleService.CreateVehicle(vehicleRequest);

            Assert.NotNull(addResult);
            Assert.Equal("Chassis2", addResult.ChassisId);
            Assert.Equal(3, addResult.ChassisNumber);
            Assert.Equal("ChassisSeries", addResult.ChassisSeries);
            Assert.Equal(1, addResult.VehicleTypeId);
            Assert.Equal(42, addResult.Passengers);
            Assert.Equal("Red", addResult.Color);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public void DuplicateChassis_ShouldError()
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
                    Chassis = chassis,
                    VehicleType = VehicleType.Truck,
                    Color = "Red"
                }
            }
            .AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(_testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(_testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "Chassis1",
                ChassisNumber = 1,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = (int)VehicleType.Bus,
                Color = "Red"
            };

            var exception = Assert.Throws<Exception>(() => vehicleService.CreateVehicle(vehicleRequest));

            Assert.Equal("Chassis already exists", exception.Message);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public void InvalidVehicleType_ShouldError()
        {
            var testVehicles = new List<Vehicle>().AsQueryable();
            var testChasses = new List<Chassis>().AsQueryable();

            _context.Setup(c => c.Vehicles).ReturnsDbSet(testVehicles);
            _context.Setup(c => c.Chasses).ReturnsDbSet(testChasses);

            var vehicleService = new VehicleService(_context.Object);

            var vehicleRequest = new VehicleRequest
            {
                ChassisId = "TestChassis",
                ChassisNumber = 1,
                ChassisSeries = "ChassisSeries",
                VehicleTypeId = 4,
                Color = "Red"
            };

            var exception = Assert.Throws<Exception>(() => vehicleService.CreateVehicle(vehicleRequest));

            Assert.Equal("Vehicle type is invalid", exception.Message);

            _context.Verify(v => v.Vehicles.Add(It.IsAny<Vehicle>()), Times.Never);
        }
    }
}