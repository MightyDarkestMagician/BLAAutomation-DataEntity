using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLAAutomation.Models
{
    public class EquipmentParameters
    {
        [Key]
        public string EquipmentModel { get; set; }
        public double PowerConsumption { get; set; }
        public double NoiseImmunity { get; set; }
        public double Weight { get; set; }

        public ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
    }
}
