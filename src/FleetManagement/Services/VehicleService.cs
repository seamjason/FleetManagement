using FleetManagement.Models;
using FleetManagement.Models.Entities;
using FleetManagement.Models.Enums;
using AppContext = FleetManagement.Configuration.AppContext;

namespace FleetManagement.Services
{
    public class VehicleService
    {
        public readonly AppContext _context;
        public VehicleService(AppContext context)
        {
            _context = context;
        }

        #region "Vehicle Service Methods"
        public VehicleResponse CreateVehicle(VehicleRequest request)
        {
            try
            {
                // Validate Vehicle Type
                if (!Enum.IsDefined(typeof(VehicleType), request.VehicleTypeId))
                {
                    throw new Exception("Vehicle type is invalid");
                }

                // Validate Chassis
                var chassis = _context.Chasses.Where(c => c.ChassisId == request.ChassisId).FirstOrDefault();

                if (chassis != null)
                {
                    if (_context.Vehicles.Where(v => v.Chassis.ChassisId == chassis.ChassisId).Any())
                    {
                        throw new Exception("Chassis already exists");
                    }
                }
                else
                {
                    chassis = new Chassis
                    {
                        ChassisId = request.ChassisId,
                        Series = request.ChassisSeries,
                        ChassisNumber = request.ChassisNumber
                    };
                    _context.Chasses.Add(chassis);
                }

                var vehicle = new Vehicle
                {
                    Chassis = chassis,
                    VehicleType = (VehicleType)request.VehicleTypeId,
                    Passengers = GetPassengersByVehicleType((VehicleType)request.VehicleTypeId),
                    Color = request.Color
                };
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();

                var response = new VehicleResponse
                {
                    Id = vehicle.Id,
                    ChassisId = vehicle.Chassis.ChassisId,
                    ChassisSeries = vehicle.Chassis.Series,
                    ChassisNumber = vehicle.Chassis.ChassisNumber,
                    VehicleTypeId = ((int)vehicle.VehicleType),
                    VehicleType = vehicle.VehicleType.ToString(),
                    Passengers = vehicle.Passengers,
                    Color = vehicle.Color
                };

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                var vehicle = _context.Vehicles.Where(v => v.Id == vehicleId).FirstOrDefault();

                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found");
                }
                else
                {
                    _context.Vehicles.Remove(vehicle);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VehicleResponse UpdateVehicle(VehicleRequest request)
        {
            try
            {
                var vehicle = _context.Vehicles.Where(v => v.Id == request.Id).FirstOrDefault();

                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found");
                }
                else
                {
                    vehicle.Color = request.Color;

                    _context.SaveChanges();

                    var response = new VehicleResponse
                    {
                        Id = vehicle.Id,
                        ChassisId = vehicle.Chassis.ChassisId,
                        ChassisSeries = vehicle.Chassis.Series,
                        ChassisNumber = vehicle.Chassis.ChassisNumber,
                        VehicleTypeId = ((int)vehicle.VehicleType),
                        VehicleType = vehicle.VehicleType.ToString(),
                        Passengers = vehicle.Passengers,
                        Color = vehicle.Color
                    };

                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VehicleResponse GetVehicleByChassisId(string chassisId)
        {
            try
            {
                var vehicle = _context.Vehicles.Where(v => v.Chassis.ChassisId == chassisId).FirstOrDefault();

                if (vehicle == null)
                {
                    throw new Exception("Vehicle Chassis not found");
                }
                else
                {
                    var response = new VehicleResponse
                    {
                        Id = vehicle.Id,
                        ChassisId = vehicle.Chassis.ChassisId,
                        ChassisSeries = vehicle.Chassis.Series,
                        ChassisNumber = vehicle.Chassis.ChassisNumber,
                        VehicleTypeId = ((int)vehicle.VehicleType),
                        VehicleType = vehicle.VehicleType.ToString(),
                        Passengers = vehicle.Passengers,
                        Color = vehicle.Color
                    };

                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VehicleResponse> GetVehicles()
        {
            var vehicles = _context.Vehicles
                .Select(v => new VehicleResponse
                {
                    Id = v.Id,
                    ChassisId = v.Chassis.ChassisId,
                    ChassisSeries = v.Chassis.Series,
                    ChassisNumber = v.Chassis.ChassisNumber,
                    VehicleTypeId = ((int)v.VehicleType),
                    VehicleType = v.VehicleType.ToString(),
                    Passengers = v.Passengers,
                    Color = v.Color
                });

            return vehicles.ToList();
        }
        #endregion

        #region "Vehcile Type Methods"

        public List<VehicleTypeResponse> GetVehicleTypes()
        {
            var vehicleTypes = Enum.GetValues(typeof(VehicleType));
            var response = new List<VehicleTypeResponse>();

            foreach (var vehicleType in vehicleTypes)
            {
                var type = new VehicleTypeResponse
                {
                    Id = ((int)((VehicleType)vehicleType)),
                    Description = ((VehicleType)vehicleType).ToString()
                };

                response.Add(type);
            }

            return response;
        }
        #endregion

        #region "Helper Functions"
        private int GetPassengersByVehicleType(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Bus: return 42;
                case VehicleType.Truck: return 1;
                case VehicleType.Car: return 4;
                default: return 0;
            }
        }
        #endregion
    }
}

