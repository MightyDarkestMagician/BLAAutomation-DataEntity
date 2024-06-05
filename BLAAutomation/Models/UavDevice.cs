using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLAAutomation.Models
{
    public class UavDevice
    {
        [Key]
        public string DeviceModel { get; set; }
        public double Weight { get; set; }
        public double NoiseImmunity { get; set; }

        public virtual ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
    }
}
