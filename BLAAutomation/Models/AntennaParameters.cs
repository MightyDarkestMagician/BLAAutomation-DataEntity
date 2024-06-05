using System.ComponentModel.DataAnnotations;

namespace BLAAutomation.Models
{
    public class AntennaParameters
    {
        [Key]
        public string AntennaMark { get; set; }
        public double FrequencyRange { get; set; }
        public double Gain { get; set; }
        public double Power { get; set; }
        public double Impedance { get; set; }
    }
}
