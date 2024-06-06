using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLAAutomation.Models;
using LiveCharts.Wpf.Charts.Base;

namespace BLAAutomation
{
    public class UavDatabaseInitializer : DropCreateDatabaseIfModelChanges<UavContext>
    {
        protected override void Seed(UavContext context)
        {
            // UAV Parameters
            var uavParameters = new[]
            {
                new UavParameters { UavModel = "UAV1", Name = "UAV Model 1", TotalCompartments = 4, Weight = 1000 },
                new UavParameters { UavModel = "UAV2", Name = "UAV Model 2", TotalCompartments = 4, Weight = 1100 },
                new UavParameters { UavModel = "UAV3", Name = "UAV Model 3", TotalCompartments = 4, Weight = 1200 },
                new UavParameters { UavModel = "UAV4", Name = "UAV Model 4", TotalCompartments = 4, Weight = 1300 }
            };
            context.UavParameters.AddRange(uavParameters);

            // Projects
            var projects = new[]
            {
                new Project { ProjectNumber = 1, ProjectName = "Project 1", UavModel = "UAV1" },
                new Project { ProjectNumber = 2, ProjectName = "Project 2", UavModel = "UAV2" },
                new Project { ProjectNumber = 3, ProjectName = "Project 3", UavModel = "UAV3" },
                new Project { ProjectNumber = 4, ProjectName = "Project 4", UavModel = "UAV4" }
            };
            context.Projects.AddRange(projects);

            // Compartments
            var compartments = new[]
            {
                // Project 1 Compartments
                new UavCompartment { CompartmentNumber = 1, UavModel = "UAV1", LoadCapacity = 100, CoordinateX = 2, CoordinateY = 3, CoordinateZ = 1, Length = 5, Width = 2, Height = 1.5 },
                new UavCompartment { CompartmentNumber = 2, UavModel = "UAV1", LoadCapacity = 150, CoordinateX = 5, CoordinateY = 1, CoordinateZ = 2, Length = 5, Width = 2, Height = 1.5 },
                new UavCompartment { CompartmentNumber = 3, UavModel = "UAV1", LoadCapacity = 200, CoordinateX = 3, CoordinateY = 4, CoordinateZ = 5, Length = 5, Width = 2, Height = 1.5 },
                new UavCompartment { CompartmentNumber = 4, UavModel = "UAV1", LoadCapacity = 120, CoordinateX = 1, CoordinateY = 2, CoordinateZ = 3, Length = 5, Width = 2, Height = 1.5 },

                // Project 2 Compartments
                new UavCompartment { CompartmentNumber = 5, UavModel = "UAV2", LoadCapacity = 110, CoordinateX = 3, CoordinateY = 2, CoordinateZ = 1, Length = 6, Width = 3, Height = 1.7 },
                new UavCompartment { CompartmentNumber = 6, UavModel = "UAV2", LoadCapacity = 160, CoordinateX = 6, CoordinateY = 2, CoordinateZ = 3, Length = 6, Width = 3, Height = 1.7 },
                new UavCompartment { CompartmentNumber = 7, UavModel = "UAV2", LoadCapacity = 210, CoordinateX = 4, CoordinateY = 5, CoordinateZ = 6, Length = 6, Width = 3, Height = 1.7 },
                new UavCompartment { CompartmentNumber = 8, UavModel = "UAV2", LoadCapacity = 130, CoordinateX = 2, CoordinateY = 3, CoordinateZ = 4, Length = 6, Width = 3, Height = 1.7 },

                // Project 3 Compartments
                new UavCompartment { CompartmentNumber = 9, UavModel = "UAV3", LoadCapacity = 120, CoordinateX = 4, CoordinateY = 3, CoordinateZ = 2, Length = 7, Width = 3.5, Height = 1.8 },
                new UavCompartment { CompartmentNumber = 10, UavModel = "UAV3", LoadCapacity = 170, CoordinateX = 7, CoordinateY = 3, CoordinateZ = 4, Length = 7, Width = 3.5, Height = 1.8 },
                new UavCompartment { CompartmentNumber = 11, UavModel = "UAV3", LoadCapacity = 220, CoordinateX = 5, CoordinateY = 6, CoordinateZ = 7, Length = 7, Width = 3.5, Height = 1.8 },
                new UavCompartment { CompartmentNumber = 12, UavModel = "UAV3", LoadCapacity = 140, CoordinateX = 3, CoordinateY = 4, CoordinateZ = 5, Length = 7, Width = 3.5, Height = 1.8 },

                // Project 4 Compartments
                new UavCompartment { CompartmentNumber = 13, UavModel = "UAV4", LoadCapacity = 130, CoordinateX = 5, CoordinateY = 4, CoordinateZ = 3, Length = 8, Width = 4, Height = 2 },
                new UavCompartment { CompartmentNumber = 14, UavModel = "UAV4", LoadCapacity = 180, CoordinateX = 8, CoordinateY = 4, CoordinateZ = 5, Length = 8, Width = 4, Height = 2 },
                new UavCompartment { CompartmentNumber = 15, UavModel = "UAV4", LoadCapacity = 230, CoordinateX = 6, CoordinateY = 7, CoordinateZ = 8, Length = 8, Width = 4, Height = 2 },
                new UavCompartment { CompartmentNumber = 16, UavModel = "UAV4", LoadCapacity = 150, CoordinateX = 4, CoordinateY = 5, CoordinateZ = 6, Length = 8, Width = 4, Height = 2 }
            };
            context.UavCompartments.AddRange(compartments);

            // Antennas
            var antennaParameters = new[]
            {
                new AntennaParameters { AntennaMark = "Antenna1", FrequencyRange = 1000000000, Gain = 1, Power = 1, Impedance = 50 },
                new AntennaParameters { AntennaMark = "Antenna2", FrequencyRange = 1000000000, Gain = 1, Power = 1, Impedance = 50 }
            };
            context.AntennaParameters.AddRange(antennaParameters);

            var uavAntennas = new[]
            {
                new UavAntenna { Id = 1, AntennaMark = "Antenna1", UavModel = "UAV1", AntennaModel = "AntennaModel1", Name = "Antenna 1", CoordinateX = 0, CoordinateY = 0, CoordinateZ = 0 },
                new UavAntenna { Id = 2, AntennaMark = "Antenna2", UavModel = "UAV1", AntennaModel = "AntennaModel2", Name = "Antenna 2", CoordinateX = 10, CoordinateY = 10, CoordinateZ = 10 }
            };
            context.UavAntennas.AddRange(uavAntennas);

            // Equipment
            var equipmentParameters = new[]
            {
                new EquipmentParameters { EquipmentModel = "Equipment1", PowerConsumption = 0, NoiseImmunity = 10, Weight = 20 },
                new EquipmentParameters { EquipmentModel = "Equipment2", PowerConsumption = 0, NoiseImmunity = 15, Weight = 30 },
                new EquipmentParameters { EquipmentModel = "Equipment3", PowerConsumption = 0, NoiseImmunity = 12, Weight = 40 },
                new EquipmentParameters { EquipmentModel = "Equipment4", PowerConsumption = 0, NoiseImmunity = 18, Weight = 50 },
                new EquipmentParameters { EquipmentModel = "Equipment5", PowerConsumption = 0, NoiseImmunity = 11, Weight = 35 },
                new EquipmentParameters { EquipmentModel = "Equipment6", PowerConsumption = 0, NoiseImmunity = 14, Weight = 25 }
            };
            context.EquipmentParameters.AddRange(equipmentParameters);

            // Devices
            var uavDevices = new[]
            {
                new UavDevice { DeviceModel = "Device1", Weight = 200, NoiseImmunity = 0.95 },
                new UavDevice { DeviceModel = "Device2", Weight = 250, NoiseImmunity = 0.85 },
                new UavDevice { DeviceModel = "Device3", Weight = 300, NoiseImmunity = 0.75 },
                new UavDevice { DeviceModel = "Device4", Weight = 220, NoiseImmunity = 0.90 },
                new UavDevice { DeviceModel = "Device5", Weight = 270, NoiseImmunity = 0.80 },
                new UavDevice { DeviceModel = "Device6", Weight = 320, NoiseImmunity = 0.70 },
                new UavDevice { DeviceModel = "Device7", Weight = 230, NoiseImmunity = 0.88 },
                new UavDevice { DeviceModel = "Device8", Weight = 280, NoiseImmunity = 0.78 },
                new UavDevice { DeviceModel = "Device9", Weight = 330, NoiseImmunity = 0.68 },
                new UavDevice { DeviceModel = "Device10", Weight = 240, NoiseImmunity = 0.87 },
                new UavDevice { DeviceModel = "Device11", Weight = 290, NoiseImmunity = 0.77 },
                new UavDevice { DeviceModel = "Device12", Weight = 340, NoiseImmunity = 0.67 }
            };
            context.UavDevices.AddRange(uavDevices);

            // Positions for Placement
            var positions = new[]
            {
                new PositionForPlacement { Id_PositionInFuselage = 1, Id_Fuselage = 1, CoordinateX = 0, CoordinateY = 1, CoordinateZ = 2 },
                new PositionForPlacement { Id_PositionInFuselage = 2, Id_Fuselage = 1, CoordinateX = 3, CoordinateY = 4, CoordinateZ = 5 },
                new PositionForPlacement { Id_PositionInFuselage = 3, Id_Fuselage = 1, CoordinateX = 6, CoordinateY = 7, CoordinateZ = 8 },
                new PositionForPlacement { Id_PositionInFuselage = 4, Id_Fuselage = 1, CoordinateX = 9, CoordinateY = 10, CoordinateZ = 11 },
                new PositionForPlacement { Id_PositionInFuselage = 5, Id_Fuselage = 2, CoordinateX = 12, CoordinateY = 13, CoordinateZ = 14 },
                new PositionForPlacement { Id_PositionInFuselage = 6, Id_Fuselage = 2, CoordinateX = 15, CoordinateY = 16, CoordinateZ = 17 },
                new PositionForPlacement { Id_PositionInFuselage = 7, Id_Fuselage = 2, CoordinateX = 18, CoordinateY = 19, CoordinateZ = 20 },
                new PositionForPlacement { Id_PositionInFuselage = 8, Id_Fuselage = 2, CoordinateX = 21, CoordinateY = 22, CoordinateZ = 23 },
                new PositionForPlacement { Id_PositionInFuselage = 9, Id_Fuselage = 3, CoordinateX = 24, CoordinateY = 25, CoordinateZ = 26 },
                new PositionForPlacement { Id_PositionInFuselage = 10, Id_Fuselage = 3, CoordinateX = 27, CoordinateY = 28, CoordinateZ = 29 },
                new PositionForPlacement { Id_PositionInFuselage = 11, Id_Fuselage = 3, CoordinateX = 30, CoordinateY = 31, CoordinateZ = 32 },
                new PositionForPlacement { Id_PositionInFuselage = 12, Id_Fuselage = 3, CoordinateX = 33, CoordinateY = 34, CoordinateZ = 35 },
                new PositionForPlacement { Id_PositionInFuselage = 13, Id_Fuselage = 4, CoordinateX = 36, CoordinateY = 37, CoordinateZ = 38 },
                new PositionForPlacement { Id_PositionInFuselage = 14, Id_Fuselage = 4, CoordinateX = 39, CoordinateY = 40, CoordinateZ = 41 },
                new PositionForPlacement { Id_PositionInFuselage = 15, Id_Fuselage = 4, CoordinateX = 42, CoordinateY = 43, CoordinateZ = 44 },
                new PositionForPlacement { Id_PositionInFuselage = 16, Id_Fuselage = 4, CoordinateX = 45, CoordinateY = 46, CoordinateZ = 47 }
            };
            context.PositionsForPlacement.AddRange(positions);

            // Equipment Placement Schemes
            var equipmentPlacementSchemes = new[]
            {
                new EquipmentPlacementScheme { SchemeNumber = 1, EquipmentModel = "Equipment1", DeviceModel = "Device1", CompartmentNumber = 1, SchemeDescription = "Scheme 1 Description", CreationDate = DateTime.Now, UavModel = "UAV1" },
                new EquipmentPlacementScheme { SchemeNumber = 2, EquipmentModel = "Equipment2", DeviceModel = "Device2", CompartmentNumber = 2, SchemeDescription = "Scheme 2 Description", CreationDate = DateTime.Now, UavModel = "UAV2" },
                new EquipmentPlacementScheme { SchemeNumber = 3, EquipmentModel = "Equipment3", DeviceModel = "Device3", CompartmentNumber = 3, SchemeDescription = "Scheme 3 Description", CreationDate = DateTime.Now, UavModel = "UAV3" },
                new EquipmentPlacementScheme { SchemeNumber = 4, EquipmentModel = "Equipment4", DeviceModel = "Device4", CompartmentNumber = 4, SchemeDescription = "Scheme 4 Description", CreationDate = DateTime.Now, UavModel = "UAV4" },
                new EquipmentPlacementScheme { SchemeNumber = 5, EquipmentModel = "Equipment5", DeviceModel = "Device5", CompartmentNumber = 5, SchemeDescription = "Scheme 5 Description", CreationDate = DateTime.Now, UavModel = "UAV1" },
                new EquipmentPlacementScheme { SchemeNumber = 6, EquipmentModel = "Equipment6", DeviceModel = "Device6", CompartmentNumber = 6, SchemeDescription = "Scheme 6 Description", CreationDate = DateTime.Now, UavModel = "UAV2" },
                new EquipmentPlacementScheme { SchemeNumber = 7, EquipmentModel = "Equipment1", DeviceModel = "Device7", CompartmentNumber = 7, SchemeDescription = "Scheme 7 Description", CreationDate = DateTime.Now, UavModel = "UAV3" },
                new EquipmentPlacementScheme { SchemeNumber = 8, EquipmentModel = "Equipment2", DeviceModel = "Device8", CompartmentNumber = 8, SchemeDescription = "Scheme 8 Description", CreationDate = DateTime.Now, UavModel = "UAV4" },
                new EquipmentPlacementScheme { SchemeNumber = 9, EquipmentModel = "Equipment3", DeviceModel = "Device9", CompartmentNumber = 9, SchemeDescription = "Scheme 9 Description", CreationDate = DateTime.Now, UavModel = "UAV1" },
                new EquipmentPlacementScheme { SchemeNumber = 10, EquipmentModel = "Equipment4", DeviceModel = "Device10", CompartmentNumber = 10, SchemeDescription = "Scheme 10 Description", CreationDate = DateTime.Now, UavModel = "UAV2" },
                new EquipmentPlacementScheme { SchemeNumber = 11, EquipmentModel = "Equipment5", DeviceModel = "Device11", CompartmentNumber = 11, SchemeDescription = "Scheme 11 Description", CreationDate = DateTime.Now, UavModel = "UAV3" },
                new EquipmentPlacementScheme { SchemeNumber = 12, EquipmentModel = "Equipment6", DeviceModel = "Device12", CompartmentNumber = 12, SchemeDescription = "Scheme 12 Description", CreationDate = DateTime.Now, UavModel = "UAV4" }
            };
            context.EquipmentPlacementSchemes.AddRange(equipmentPlacementSchemes);

            // Devices for Placement
            var devicesForPlacement = new[]
            {
                // Project 1 Devices
                new DeviceForPlacement { Id_Project = 1, Id_Device = "Device1" },
                new DeviceForPlacement { Id_Project = 1, Id_Device = "Device2" },
                new DeviceForPlacement { Id_Project = 1, Id_Device = "Device3" },
                new DeviceForPlacement { Id_Project = 1, Id_Device = "Device4" },

                // Project 2 Devices
                new DeviceForPlacement { Id_Project = 2, Id_Device = "Device5" },
                new DeviceForPlacement { Id_Project = 2, Id_Device = "Device6" },
                new DeviceForPlacement { Id_Project = 2, Id_Device = "Device7" },
                new DeviceForPlacement { Id_Project = 2, Id_Device = "Device8" },

                // Project 3 Devices
                new DeviceForPlacement { Id_Project = 3, Id_Device = "Device9" },
                new DeviceForPlacement { Id_Project = 3, Id_Device = "Device10" },
                new DeviceForPlacement { Id_Project = 3, Id_Device = "Device11" },
                new DeviceForPlacement { Id_Project = 3, Id_Device = "Device12" },

                // Project 4 Devices
                new DeviceForPlacement { Id_Project = 4, Id_Device = "Device1" },
                new DeviceForPlacement { Id_Project = 4, Id_Device = "Device2" },
                new DeviceForPlacement { Id_Project = 4, Id_Device = "Device3" },
                new DeviceForPlacement { Id_Project = 4, Id_Device = "Device4" }
            };
            context.DevicesForPlacement.AddRange(devicesForPlacement);

            // Landing Sites
            var landingSites = new[]
            {
                new LandingSite { LandingSiteNumber = 1, UavModel = "UAV1", CoordinateX = 0, CoordinateY = 0, CoordinateZ = 0, WeightLimit = 2000 },
                new LandingSite { LandingSiteNumber = 2, UavModel = "UAV2", CoordinateX = 10, CoordinateY = 10, CoordinateZ = 10, WeightLimit = 2500 },
                new LandingSite { LandingSiteNumber = 3, UavModel = "UAV3", CoordinateX = 20, CoordinateY = 20, CoordinateZ = 20, WeightLimit = 3000 },
                new LandingSite { LandingSiteNumber = 4, UavModel = "UAV4", CoordinateX = 30, CoordinateY = 30, CoordinateZ = 30, WeightLimit = 3500 }
            };
            context.LandingSites.AddRange(landingSites);

            // Fuselages
            var fuselages = new[]
            {
                new Fuselage
                {
                    Id = 1,
                    Name = "Fuselage 1",
                    AntennasInFuselage = new List<AntennaInFuselage>(uavAntennas.Select(a => new AntennaInFuselage { Id_Antenna = a.AntennaMark, Id_Fuselage = 1, CoordinateX = a.CoordinateX, CoordinateY = a.CoordinateY, CoordinateZ = a.CoordinateZ })),
                    CompartmentsInFuselage = new List<CompartmentInFuselage>(compartments.Take(4).Select(c => new CompartmentInFuselage { Id_Compartment = c.CompartmentNumber, Id_Fuselage = 1, CoordinateX = c.CoordinateX, CoordinateY = c.CoordinateY, CoordinateZ = c.CoordinateZ, Length = c.Length, Width = c.Width, Height = c.Height, Carrying = c.LoadCapacity })),
                    PositionsForPlacement = new List<PositionForPlacement>(positions.Where(p => p.Id_Fuselage == 1))
                },
                new Fuselage
                {
                    Id = 2,
                    Name = "Fuselage 2",
                    AntennasInFuselage = new List<AntennaInFuselage>(uavAntennas.Select(a => new AntennaInFuselage { Id_Antenna = a.AntennaMark, Id_Fuselage = 2, CoordinateX = a.CoordinateX, CoordinateY = a.CoordinateY, CoordinateZ = a.CoordinateZ })),
                    CompartmentsInFuselage = new List<CompartmentInFuselage>(compartments.Skip(4).Take(4).Select(c => new CompartmentInFuselage { Id_Compartment = c.CompartmentNumber, Id_Fuselage = 2, CoordinateX = c.CoordinateX, CoordinateY = c.CoordinateY, CoordinateZ = c.CoordinateZ, Length = c.Length, Width = c.Width, Height = c.Height, Carrying = c.LoadCapacity })),
                    PositionsForPlacement = new List<PositionForPlacement>(positions.Where(p => p.Id_Fuselage == 2))
                },
                new Fuselage
                {
                    Id = 3,
                    Name = "Fuselage 3",
                    AntennasInFuselage = new List<AntennaInFuselage>(uavAntennas.Select(a => new AntennaInFuselage { Id_Antenna = a.AntennaMark, Id_Fuselage = 3, CoordinateX = a.CoordinateX, CoordinateY = a.CoordinateY, CoordinateZ = a.CoordinateZ })),
                    CompartmentsInFuselage = new List<CompartmentInFuselage>(compartments.Skip(8).Take(4).Select(c => new CompartmentInFuselage { Id_Compartment = c.CompartmentNumber, Id_Fuselage = 3, CoordinateX = c.CoordinateX, CoordinateY = c.CoordinateY, CoordinateZ = c.CoordinateZ, Length = c.Length, Width = c.Width, Height = c.Height, Carrying = c.LoadCapacity })),
                    PositionsForPlacement = new List<PositionForPlacement>(positions.Where(p => p.Id_Fuselage == 3))
                },
                new Fuselage
                {
                    Id = 4,
                    Name = "Fuselage 4",
                    AntennasInFuselage = new List<AntennaInFuselage>(uavAntennas.Select(a => new AntennaInFuselage { Id_Antenna = a.AntennaMark, Id_Fuselage = 4, CoordinateX = a.CoordinateX, CoordinateY = a.CoordinateY, CoordinateZ = a.CoordinateZ })),
                    CompartmentsInFuselage = new List<CompartmentInFuselage>(compartments.Skip(12).Take(4).Select(c => new CompartmentInFuselage { Id_Compartment = c.CompartmentNumber, Id_Fuselage = 4, CoordinateX = c.CoordinateX, CoordinateY = c.CoordinateY, CoordinateZ = c.CoordinateZ, Length = c.Length, Width = c.Width, Height = c.Height, Carrying = c.LoadCapacity })),
                    PositionsForPlacement = new List<PositionForPlacement>(positions.Where(p => p.Id_Fuselage == 4))
                }
            };
            context.Fuselages.AddRange(fuselages);

            context.SaveChanges();
        }
    }
}
