using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLAAutomation.Models
{
    public class UavAntenna
    {
        [Key]
        public string AntennaMark { get; set; }
        public string UavModel { get; set; }
        public string AntennaModel { get; set; }
        public string Name { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }

        [ForeignKey("UavModel")]
        public UavParameters UavParameters { get; set; }
    }
}
