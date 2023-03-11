using System.ComponentModel;

namespace FleetManagement.Models.Enums
{
    public enum VehicleType
    {
        [Description("Bus")]
        Bus = 1,

        [Description("Truck")]
        Truck = 2,

        [Description("Car")]
        Car = 3
    };
}