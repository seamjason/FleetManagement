using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Models.Entities
{
    [Table("Chassis")]
    public class Chassis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? ChassisId { get; set; }

        public string? Series { get; set; }

        public int ChassisNumber { get; set; }
    }
}
