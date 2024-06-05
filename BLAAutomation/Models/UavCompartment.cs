using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLAAutomation.Models
{
    public class UavCompartment
    {
        [Key]
        public int CompartmentNumber { get; set; }
        public string UavModel { get; set; }
        public double LoadCapacity { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        [ForeignKey("UavModel")]
        public UavParameters UavParameters { get; set; }
        public ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
    }
}
