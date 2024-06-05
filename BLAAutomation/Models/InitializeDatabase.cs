using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BLAAutomation.Models
{
    public class InitializeDatabase : DropCreateDatabaseIfModelChanges<UavContext>
    {
        protected override void Seed(UavContext context)
        {
            var projects = new List<Project>
            {
                new Project { ProjectNumber = 1, ProjectName = "Project 1", UavModel = "UAV1" },
                new Project { ProjectNumber = 2, ProjectName = "Project 2", UavModel = "UAV2" },
                new Project { ProjectNumber = 3, ProjectName = "Project 3", UavModel = "UAV3" }
            };

            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();

            var uavParameters = new List<UavParameters>
            {
                new UavParameters { UavModel = "UAV1", Name = "UAV Model 1", TotalCompartments = 3, Weight = 150.0 },
                new UavParameters { UavModel = "UAV2", Name = "UAV Model 2", TotalCompartments = 4, Weight = 200.0 },
                new UavParameters { UavModel = "UAV3", Name = "UAV Model 3", TotalCompartments = 5, Weight = 250.0 }
            };

            uavParameters.ForEach(u => context.UavParameters.Add(u));
            context.SaveChanges();

            var uavCompartments = new List<UavCompartment>
            {
                new UavCompartment { CompartmentNumber = 1, UavModel = "UAV1", LoadCapacity = 50.0, CoordinateX = 0.0, CoordinateY = 0.0, CoordinateZ = 0.0, Length = 5.0, Width = 4.0, Height = 3.0 },
                new UavCompartment { CompartmentNumber = 2, UavModel = "UAV1", LoadCapacity = 60.0, CoordinateX = 10.0, CoordinateY = 0.0, CoordinateZ = 0.0, Length = 6.0, Width = 5.0, Height = 4.0 },
                new UavCompartment { CompartmentNumber = 3, UavModel = "UAV1", LoadCapacity = 70.0, CoordinateX = 20.0, CoordinateY = 0.0, CoordinateZ = 0.0, Length = 7.0, Width = 6.0, Height = 5.0 },
                new UavCompartment { CompartmentNumber = 4, UavModel = "UAV2", LoadCapacity = 80.0, CoordinateX = 0.0, CoordinateY = 10.0, CoordinateZ = 0.0, Length = 8.0, Width = 7.0, Height = 6.0 },
                new UavCompartment { CompartmentNumber = 5, UavModel = "UAV2", LoadCapacity = 90.0, CoordinateX = 10.0, CoordinateY = 10.0, CoordinateZ = 0.0, Length = 9.0, Width = 8.0, Height = 7.0 },
                new UavCompartment { CompartmentNumber = 6, UavModel = "UAV2", LoadCapacity = 100.0, CoordinateX = 20.0, CoordinateY = 10.0, CoordinateZ = 0.0, Length = 10.0, Width = 9.0, Height = 8.0 },
                new UavCompartment { CompartmentNumber = 7, UavModel = "UAV3", LoadCapacity = 110.0, CoordinateX = 0.0, CoordinateY = 20.0, CoordinateZ = 0.0, Length = 11.0, Width = 10.0, Height = 9.0 },
                new UavCompartment { CompartmentNumber = 8, UavModel = "UAV3", LoadCapacity = 120.0, CoordinateX = 10.0, CoordinateY = 20.0, CoordinateZ = 0.0, Length = 12.0, Width = 11.0, Height = 10.0 },
                new UavCompartment { CompartmentNumber = 9, UavModel = "UAV3", LoadCapacity = 130.0, CoordinateX = 20.0, CoordinateY = 20.0, CoordinateZ = 0.0, Length = 13.0, Width = 12.0, Height = 11.0 }
            };

            uavCompartments.ForEach(c => context.UavCompartments.Add(c));
            context.SaveChanges();

            var equipmentParameters = new List<EquipmentParameters>
            {
                new EquipmentParameters { EquipmentModel = "EQUIP1", PowerConsumption = 20.0, NoiseImmunity = 30.0, Weight = 10.0 },
                new EquipmentParameters { EquipmentModel = "EQUIP2", PowerConsumption = 25.0, NoiseImmunity = 35.0, Weight = 15.0 },
                new EquipmentParameters { EquipmentModel = "EQUIP3", PowerConsumption = 30.0, NoiseImmunity = 40.0, Weight = 20.0 },
                new EquipmentParameters { EquipmentModel = "EQUIP4", PowerConsumption = 35.0, NoiseImmunity = 45.0, Weight = 25.0 },
                new EquipmentParameters { EquipmentModel = "EQUIP5", PowerConsumption = 40.0, NoiseImmunity = 50.0, Weight = 30.0 },
                new EquipmentParameters { EquipmentModel = "EQUIP6", PowerConsumption = 45.0, NoiseImmunity = 55.0, Weight = 35.0 }
            };

            equipmentParameters.ForEach(e => context.EquipmentParameters.Add(e));
            context.SaveChanges();

            var antennaParameters = new List<AntennaParameters>
            {
                new AntennaParameters { AntennaMark = "ANT1", FrequencyRange = 100.0, Gain = 20.0, Power = 15.0, Impedance = 50.0 },
                new AntennaParameters { AntennaMark = "ANT2", FrequencyRange = 200.0, Gain = 25.0, Power = 20.0, Impedance = 60.0 },
                new AntennaParameters { AntennaMark = "ANT3", FrequencyRange = 300.0, Gain = 30.0, Power = 25.0, Impedance = 70.0 }
            };

            antennaParameters.ForEach(a => context.AntennaParameters.Add(a));
            context.SaveChanges();

            var uavAntennas = new List<UavAntenna>
            {
                new UavAntenna { AntennaMark = "ANT1", UavModel = "UAV1", AntennaModel = "Model1", Name = "Antenna 1", CoordinateX = 0.0, CoordinateY = 1.0, CoordinateZ = 2.0 },
                new UavAntenna { AntennaMark = "ANT2", UavModel = "UAV2", AntennaModel = "Model2", Name = "Antenna 2", CoordinateX = 3.0, CoordinateY = 4.0, CoordinateZ = 5.0 },
                new UavAntenna { AntennaMark = "ANT3", UavModel = "UAV3", AntennaModel = "Model3", Name = "Antenna 3", CoordinateX = 6.0, CoordinateY = 7.0, CoordinateZ = 8.0 }
            };

            uavAntennas.ForEach(ua => context.UavAntennas.Add(ua));
            context.SaveChanges();

            var landingSites = new List<LandingSites>
            {
                new LandingSites { LandingSiteNumber = 1, UavModel = "UAV1", CoordinateX = 0.0, CoordinateY = 1.0, CoordinateZ = 2.0, WeightLimit = 100.0 },
                new LandingSites { LandingSiteNumber = 2, UavModel = "UAV2", CoordinateX = 3.0, CoordinateY = 4.0, CoordinateZ = 5.0, WeightLimit = 150.0 },
                new LandingSites { LandingSiteNumber = 3, UavModel = "UAV3", CoordinateX = 6.0, CoordinateY = 7.0, CoordinateZ = 8.0, WeightLimit = 200.0 }
            };

            landingSites.ForEach(ls => context.LandingSites.Add(ls));
            context.SaveChanges();

            var uavDevices = new List<UavDevice>
            {
                new UavDevice { DeviceModel = "DEV1", Weight = 50.0, NoiseImmunity = 60.0 },
                new UavDevice { DeviceModel = "DEV2", Weight = 70.0, NoiseImmunity = 80.0 },
                new UavDevice { DeviceModel = "DEV3", Weight = 90.0, NoiseImmunity = 100.0 }
            };

            uavDevices.ForEach(ud => context.UavDevices.Add(ud));
            context.SaveChanges();

            var equipmentPlacementSchemes = new List<EquipmentPlacementScheme>
            {
                new EquipmentPlacementScheme { SchemeNumber = 1, EquipmentModel = "EQUIP1", DeviceModel = "DEV1", CompartmentNumber = 1, SchemeDescription = "Scheme 1", CreationDate = DateTime.Now, UavModel = "UAV1" },
                new EquipmentPlacementScheme { SchemeNumber = 2, EquipmentModel = "EQUIP2", DeviceModel = "DEV2", CompartmentNumber = 2, SchemeDescription = "Scheme 2", CreationDate = DateTime.Now, UavModel = "UAV2" },
                new EquipmentPlacementScheme { SchemeNumber = 3, EquipmentModel = "EQUIP3", DeviceModel = "DEV3", CompartmentNumber = 3, SchemeDescription = "Scheme 3", CreationDate = DateTime.Now, UavModel = "UAV3" },
                new EquipmentPlacementScheme { SchemeNumber = 4, EquipmentModel = "EQUIP4", DeviceModel = "DEV1", CompartmentNumber = 4, SchemeDescription = "Scheme 4", CreationDate = DateTime.Now, UavModel = "UAV1" },
                new EquipmentPlacementScheme { SchemeNumber = 5, EquipmentModel = "EQUIP5", DeviceModel = "DEV2", CompartmentNumber = 5, SchemeDescription = "Scheme 5", CreationDate = DateTime.Now, UavModel = "UAV2" },
                new EquipmentPlacementScheme { SchemeNumber = 6, EquipmentModel = "EQUIP6", DeviceModel = "DEV3", CompartmentNumber = 6, SchemeDescription = "Scheme 6", CreationDate = DateTime.Now, UavModel = "UAV3" }
            };

            equipmentPlacementSchemes.ForEach(eps => context.EquipmentPlacementSchemes.Add(eps));
            context.SaveChanges();
        }
    }
}
