namespace FleetManagement.Models
{
    public class VehicleRequest
    {
        public int Id { get; set; }

        public string? ChassisId { get; set; }

        public string? ChassisSeries { get; set; }

        public int ChassisNumber { get; set; }

        public int VehicleTypeId { get; set; }

        public string? Color { get; set; }
    }
}