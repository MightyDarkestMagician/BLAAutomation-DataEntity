using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLAAutomation.Models
{
    public class EquipmentPlacementScheme
    {
        [Key]
        public int SchemeNumber { get; set; }
        public string EquipmentModel { get; set; }
        public string DeviceModel { get; set; }
        public int CompartmentNumber { get; set; }
        public string SchemeDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public string UavModel { get; set; }


        [ForeignKey("EquipmentModel")]
        public EquipmentParameters EquipmentParameters { get; set; }

        [ForeignKey("DeviceModel")]
        public UavDevice UavDevice { get; set; }

        [ForeignKey("CompartmentNumber")]
        public UavCompartment UavCompartment { get; set; }

        [ForeignKey("UavModel")]
        public UavParameters UavParameters { get; set; }
    }
}
