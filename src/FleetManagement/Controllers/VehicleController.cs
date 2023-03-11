using FleetManagement.Configuration;
using FleetManagement.Models;
using FleetManagement.Services;
using Microsoft.AspNetCore.Mvc;
using AppContext = FleetManagement.Configuration.AppContext;

namespace FleetManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private AppContext _context;
        private DbSettings _dbSettings;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpPost("create")]
        public VehicleResponse CreateVehicle([FromBody] VehicleRequest vehicle)
        {
            var vehicleService = GetVehicleService();
            return vehicleService.CreateVehicle(vehicle);
        }

        [HttpDelete("delete")]
        public bool DeleteVehicle(int vehicleId)
        {
            var vehicleService = GetVehicleService();
            return vehicleService.DeleteVehicle(vehicleId);
        }

        [HttpPut("update")]
        public VehicleResponse UpdateVehicle([FromBody] VehicleRequest vehicle)
        {
            var vehicleService = GetVehicleService();
            return vehicleService.UpdateVehicle(vehicle);
        }

        [HttpGet("get")]
        public VehicleResponse GetVehicle([FromRoute] string chassisId)
        {
            var vehicleService = GetVehicleService();
            return vehicleService.GetVehicleByChassisId(chassisId);
        }

        [HttpGet("getall")]
        public List<VehicleResponse> GetVehicles()
        {
            var vehicleService = GetVehicleService();
            return vehicleService.GetVehicles();
        }

        [HttpGet("types/get")]
        public List<VehicleTypeResponse> GetVehicleTypes()
        {
            var vehicleService = GetVehicleService();
            return vehicleService.GetVehicleTypes();
        }

        protected VehicleService GetVehicleService()
        {
            if (_context == null)
            {
                if (_dbSettings == null)
                {
                    // TODO: Pull connection string from appSettings on Startup

                    var connectionString = "Server=tcp:azusql1.database.windows.net,1433;Initial Catalog=LockingMechanism;Persist Security Info=False;User ID=seamadmin;Password=8eXhjJBxBEK8BGz40nkG;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                    _dbSettings = new DbSettings
                    {
                        DbConnectionString = connectionString
                    };
                }
                _context = new AppContext(_dbSettings.DbConnectionString);
            }

            return new VehicleService(_context);
        }
    }
}