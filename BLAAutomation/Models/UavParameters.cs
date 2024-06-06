using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLAAutomation.Models
{
    public class UavParameters
    {
        [Key]
        public string UavModel { get; set; }
        public string Name { get; set; }
        public int TotalCompartments { get; set; }
        public double Weight { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<UavCompartment> Compartments { get; set; }
        public ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
        public ICollection<UavAntenna> Antennas { get; set; }
        public ICollection<LandingSite> LandingSites { get; set; }
    }
}
