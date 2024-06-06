using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BLAAutomation.Models
{


    public class Project
    {
        [Key]
        public int ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string UavModel { get; set; }

        [ForeignKey("UavModel")]
        public virtual UavParameters UavParameters { get; set; }

        public virtual ICollection<DeviceForPlacement> DevicesForPlacement { get; set; }

        // Use separate collection to store Compartments data explicitly
        [NotMapped]
        public ICollection<UavCompartment> Compartments { get; set; }

        // Use separate field to store Fuselage data explicitly
        [NotMapped]
        public Fuselage SelectedFuselage { get; set; }

        public void LoadRelatedData()
        {
            try
            {
                using (var context = new UavContext())
                {
                    var project = context.Projects
                        .Include(p => p.UavParameters)
                        .Include(p => p.UavParameters.Compartments)
                        .Include(p => p.UavParameters.Fuselages)
                        .Include(p => p.DevicesForPlacement.Select(dfp => dfp.UavDevice))
                        .FirstOrDefault(p => p.ProjectNumber == this.ProjectNumber);

                    if (project != null)
                    {
                        this.UavParameters = project.UavParameters;
                        this.Compartments = project.UavParameters.Compartments.ToList();
                        this.SelectedFuselage = project.UavParameters.Fuselages.FirstOrDefault();

                        // Проверка и инициализация поля SelectedFuselage
                        if (this.SelectedFuselage == null)
                        {
                            throw new Exception("SelectedFuselage is null.");
                        }
                    }
                    else
                    {
                        throw new Exception("Project not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading related data: " + ex.Message);
                throw;
            }
        }
    }

        public class UavParameters
    {
        [Key]
        public string UavModel { get; set; }
        public string Name { get; set; }
        public int TotalCompartments { get; set; }
        public double Weight { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UavCompartment> Compartments { get; set; }
        public virtual ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
        public virtual ICollection<UavAntenna> Antennas { get; set; }
        public virtual ICollection<LandingSite> LandingSites { get; set; }
        public virtual ICollection<Fuselage> Fuselages { get; set; }
    }

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
        public virtual UavParameters UavParameters { get; set; }
    }

    public class EquipmentParameters
    {
        [Key]
        public string EquipmentModel { get; set; }
        public double PowerConsumption { get; set; }
        public double NoiseImmunity { get; set; }
        public double Weight { get; set; }

        public virtual ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
    }

    public class UavDevice
    {
        [Key]
        public string DeviceModel { get; set; }
        public double Weight { get; set; }
        public double NoiseImmunity { get; set; }

        public virtual ICollection<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
        public virtual ICollection<DeviceForPlacement> DeviceForPlacements { get; set; }
    }

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
        public virtual EquipmentParameters EquipmentParameters { get; set; }

        [ForeignKey("DeviceModel")]
        public virtual UavDevice UavDevice { get; set; }

        [ForeignKey("CompartmentNumber")]
        public virtual UavCompartment UavCompartment { get; set; }

        [ForeignKey("UavModel")]
        public virtual UavParameters UavParameters { get; set; }
    }

    public class AntennaParameters
    {
        [Key]
        public string AntennaMark { get; set; }
        public double FrequencyRange { get; set; }
        public double Gain { get; set; }
        public double Power { get; set; }
        public double Impedance { get; set; }

        public virtual ICollection<AntennaInFuselage> AntennasInFuselage { get; set; }
    }

    public class UavAntenna
    {
        [Key]
        public int Id { get; set; }
        public string AntennaMark { get; set; }
        public string UavModel { get; set; }
        public string AntennaModel { get; set; }
        public string Name { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }

        [ForeignKey("AntennaMark")]
        public virtual AntennaParameters AntennaParameters { get; set; }

        [ForeignKey("UavModel")]
        public virtual UavParameters UavParameters { get; set; }
    }

    public class LandingSite
    {
        [Key]
        public int LandingSiteNumber { get; set; }
        public string UavModel { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }
        public double WeightLimit { get; set; }

        [ForeignKey("UavModel")]
        public virtual UavParameters UavParameters { get; set; }
    }

    public class PositionForPlacement
    {
        [Key]
        public int Id_PositionInFuselage { get; set; }
        public int Id_Fuselage { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }

        [ForeignKey("Id_Fuselage")]
        public virtual Fuselage Fuselage { get; set; }

        public static List<PositionForPlacement> GetPositionsForFuselage(UavContext context, int idFuselage)
        {
            return context.PositionsForPlacement.Where(p => p.Id_Fuselage == idFuselage).ToList();
        }

        public static bool IsPositionPlacedInSelectedCompartment(CompartmentInFuselage compartment, PositionForPlacement position)
        {
            return position.CoordinateX <= (compartment.CoordinateX + compartment.Length / 2.0) &&
                   position.CoordinateX >= (compartment.CoordinateX - compartment.Length / 2.0) &&
                   position.CoordinateY <= (compartment.CoordinateY + compartment.Width / 2.0) &&
                   position.CoordinateY >= (compartment.CoordinateY - compartment.Width / 2.0) &&
                   position.CoordinateZ <= (compartment.CoordinateZ + compartment.Height / 2.0) &&
                   position.CoordinateZ >= (compartment.CoordinateZ - compartment.Height / 2.0);
        }
    }

    public class DeviceForPlacement
    {
        [Key, Column(Order = 0)]
        public int Id_Project { get; set; }
        [Key, Column(Order = 1)]
        public string Id_Device { get; set; }

        [ForeignKey("Id_Project")]
        public virtual Project Project { get; set; }

        [ForeignKey("Id_Device")]
        public virtual UavDevice UavDevice { get; set; }
    }

    public class Fuselage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AntennaInFuselage> AntennasInFuselage { get; set; }
        public virtual ICollection<CompartmentInFuselage> CompartmentsInFuselage { get; set; }
        public virtual ICollection<PositionForPlacement> PositionsForPlacement { get; set; }
        public double[,] Distances { get; set; }
        public double[,] E { get; set; }

        public Fuselage() { }

        public Fuselage(int id, UavContext context)
        {
            var fuselage = context.Fuselages.Include(f => f.AntennasInFuselage)
                                            .Include(f => f.CompartmentsInFuselage)
                                            .Include(f => f.PositionsForPlacement)
                                            .SingleOrDefault(f => f.Id == id);

            if (fuselage == null) throw new Exception("Fuselage not found.");

            Id = fuselage.Id;
            Name = fuselage.Name;
            AntennasInFuselage = fuselage.AntennasInFuselage;
            CompartmentsInFuselage = fuselage.CompartmentsInFuselage;
            PositionsForPlacement = fuselage.PositionsForPlacement;

            foreach (var tempPosition in PositionsForPlacement)
            {
                foreach (var tempCompartment in CompartmentsInFuselage)
                {
                    if (IsPositionPlacedInSelectedCompartment(tempCompartment, tempPosition))
                    {
                        tempPosition.Fuselage = this;
                        break;
                    }
                }
            }

            double c = 299792458.0;
            double eps = 1.0;

            Distances = new double[AntennasInFuselage.Count, CompartmentsInFuselage.Count];
            E = new double[AntennasInFuselage.Count, CompartmentsInFuselage.Count];
            for (int i = 0; i < AntennasInFuselage.Count; i++)
            {
                var ant = AntennasInFuselage.ElementAt(i);
                for (int j = 0; j < CompartmentsInFuselage.Count; j++)
                {
                    var cmp = CompartmentsInFuselage.ElementAt(j);
                    Distances[i, j] = Math.Sqrt((cmp.CoordinateX - ant.CoordinateX) * (cmp.CoordinateX - ant.CoordinateX) +
                                                (cmp.CoordinateY - ant.CoordinateY) * (cmp.CoordinateY - ant.CoordinateY) +
                                                (cmp.CoordinateZ - ant.CoordinateZ) * (cmp.CoordinateZ - ant.CoordinateZ));
                    double toCheck = c / (2.0 * Math.PI * ant.AntennaParameters.FrequencyRange);
                    if (Distances[i, j] < toCheck)
                    {
                        E[i, j] = (ant.AntennaParameters.Impedance * ant.AntennaParameters.Gain) /
                                  (4.0 * Math.PI * Math.PI * eps * ant.AntennaParameters.FrequencyRange * Distances[i, j] * Distances[i, j]);
                    }
                    else
                    {
                        E[i, j] = (Math.Sqrt(30.0 * ant.AntennaParameters.Power)) / Distances[i, j];
                    }
                }
            }

            for (int i = 0; i < CompartmentsInFuselage.Count; i++)
            {
                var compartment = CompartmentsInFuselage.ElementAt(i);
                compartment.Carrying = 0;
                for (int j = 0; j < AntennasInFuselage.Count; j++)
                {
                    compartment.Carrying += E[j, i];
                }
            }
        }

        public static bool IsPositionPlacedInSelectedCompartment(CompartmentInFuselage compartment, PositionForPlacement position)
        {
            return position.CoordinateX <= (compartment.CoordinateX + compartment.Length / 2.0) &&
                   position.CoordinateX >= (compartment.CoordinateX - compartment.Length / 2.0) &&
                   position.CoordinateY <= (compartment.CoordinateY + compartment.Width / 2.0) &&
                   position.CoordinateY >= (compartment.CoordinateY - compartment.Width / 2.0) &&
                   position.CoordinateZ <= (compartment.CoordinateZ + compartment.Height / 2.0) &&
                   position.CoordinateZ >= (compartment.CoordinateZ - compartment.Height / 2.0);
        }
    }

    public class CompartmentInFuselage
    {
        [Key]
        public int Id_Compartment { get; set; }
        public int Id_Fuselage { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Carrying { get; set; }

        [ForeignKey("Id_Fuselage")]
        public virtual Fuselage Fuselage { get; set; }
    }

    public class AntennaInFuselage
    {
        [Key, Column(Order = 0)]
        public string Id_Antenna { get; set; }
        [Key, Column(Order = 1)]
        public int Id_Fuselage { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double CoordinateZ { get; set; }

        [ForeignKey("Id_Antenna")]
        public virtual AntennaParameters AntennaParameters { get; set; }

        [ForeignKey("Id_Fuselage")]
        public virtual Fuselage Fuselage { get; set; }

        public static List<AntennaInFuselage> GetAntennasForFuselage(UavContext context, int idFuselage)
        {
            return context.AntennasInFuselage.Where(a => a.Id_Fuselage == idFuselage).ToList();
        }
    }
}
