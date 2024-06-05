using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLAAutomation.Models
{
    public class LandingSites
    {
        [Key]
        public int LandingSiteNumber { get; set; }
        public string UavModel { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }
        public double WeightLimit { get; set; }

        [ForeignKey("UavModel")]
        public UavParameters UavParameters { get; set; }
    }
}
