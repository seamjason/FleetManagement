using FleetManagement.Configuration;
using FleetManagement.Models;
using FleetManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using FleetManagementContext = FleetManagement.Configuration.FleetManagementContext;

namespace FleetManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        //private FleetManagementContext _context;
        //private DbSettings _dbSettings;
        private IVehicleService _service;

        public VehicleController(ILogger<VehicleController> logger, FleetManagementContext context, IVehicleService service)
        {
            _logger = logger;
            //_context = context;
            _service = service;
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
        public VehicleResponse GetVehicle(string chassisId)
        {
            var vehicleService = GetVehicleService();
            return vehicleService.GetVehicleByChassisId(chassisId);
        }

        [EnableCors]
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

        protected IVehicleService GetVehicleService()
        {
            return _service;
            //if (_context == null)
            //{

            //    if (_dbSettings == null)
            //    {
            //        // TODO: Pull connection string from appSettings on Startup

            //        var connectionString = "Server=tcp:azusql1.database.windows.net,1433;Initial Catalog=LockingMechanism;Persist Security Info=False;User ID=seamadmin;Password=8eXhjJBxBEK8BGz40nkG;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //        _dbSettings = new DbSettings
            //        {
            //            DbConnectionString = connectionString
            //        };
            //    }
            //    _context = new FleetManagementContext(_dbSettings.DbConnectionString);
            //}

            //return new VehicleService(_context);
        }
    }
}