using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLAAutomation.Models;

namespace BLAAutomation.Models
{
    public class Project
    {
        [Key]
        public int ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string UavModel { get; set; }

        [ForeignKey("UavModel")]
        public UavParameters UavParameters { get; set; }
        public ICollection<UavCompartment> Compartments { get; set; }
        public ICollection<EquipmentParameters> DevicesForPlacement { get; set; }
        public ICollection<UavAntenna> SelectedFuselage { get; set; }
    }
}
