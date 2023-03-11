using FleetManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Models.Entities
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Chassis? Chassis { get; set; }

        [Column("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }

        public int Passengers { get; set; }

        public string? Color { get; set; }
    }
}