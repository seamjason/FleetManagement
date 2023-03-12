using FleetManagement.Models;

namespace FleetManagement.Services
{
    public interface IVehicleService
    {
        public VehicleResponse CreateVehicle(VehicleRequest request);
        public bool DeleteVehicle(int vehicleId);
        public VehicleResponse UpdateVehicle(VehicleRequest request);
        public VehicleResponse GetVehicleByChassisId(string chassisId);
        public List<VehicleResponse> GetVehicles();
        public List<VehicleTypeResponse> GetVehicleTypes();
    }
}

