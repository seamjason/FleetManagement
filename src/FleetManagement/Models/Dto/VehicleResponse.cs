namespace FleetManagement.Models
{
    public class VehicleResponse
    {
        public int Id { get; set; }

        public string? ChassisId { get; set; }

        public string? ChassisSeries { get; set; }

        public int ChassisNumber { get; set; }

        public int VehicleTypeId { get; set; }

        public string? VehicleType { get; set; }

        public int Passengers { get; set; }

        public string? Color { get; set; }
    }
}