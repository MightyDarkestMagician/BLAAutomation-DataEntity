using BLAAutomation.Models;
using System.Data.Entity;

namespace PlcaementWithGenetiAlgorithm
{
    public class UavContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UavParameters> UavParameters { get; set; }
        public DbSet<UavCompartment> UavCompartments { get; set; }
        public DbSet<EquipmentParameters> EquipmentParameters { get; set; }
        public DbSet<UavDevice> UavDevices { get; set; }
        public DbSet<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
        public DbSet<AntennaParameters> AntennaParameters { get; set; }
        public DbSet<UavAntenna> UavAntennas { get; set; }
        public DbSet<LandingSite> LandingSites { get; set; }
        public DbSet<PositionForPlacement> PositionsForPlacement { get; set; }
        public DbSet<DeviceForPlacement> DevicesForPlacement { get; set; }
        public DbSet<Fuselage> Fuselages { get; set; }
        public DbSet<CompartmentInFuselage> CompartmentsInFuselage { get; set; }
        public DbSet<AntennaInFuselage> AntennaInFuselage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasRequired(p => p.UavParameters)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UavModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UavCompartment>()
                .HasRequired(c => c.UavParameters)
                .WithMany(u => u.Compartments)
                .HasForeignKey(c => c.UavModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.UavParameters)
                .WithMany(u => u.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.UavModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.EquipmentParameters)
                .WithMany(e => e.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.EquipmentModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.UavDevice)
                .WithMany(d => d.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.DeviceModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UavAntenna>()
                .HasRequired(a => a.UavParameters)
                .WithMany(u => u.Antennas)
                .HasForeignKey(a => a.UavModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LandingSite>()
                .HasRequired(l => l.UavParameters)
                .WithMany(u => u.LandingSites)
                .HasForeignKey(l => l.UavModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionForPlacement>()
                .HasRequired(p => p.Fuselage)
                .WithMany(f => f.PositionsForFuselage)
                .HasForeignKey(p => p.Id_Fuselage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceForPlacement>()
                .HasRequired(d => d.Project)
                .WithMany(p => p.DevicesForPlacement)
                .HasForeignKey(d => d.Id_Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceForPlacement>()
                .HasRequired(d => d.Device)
                .WithMany(dev => dev.DevicesForPlacement)
                .HasForeignKey(d => d.Id_Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompartmentInFuselage>()
                .HasRequired(c => c.Fuselage)
                .WithMany(f => f.CompartmentsInFuselage)
                .HasForeignKey(c => c.Id_Fuselage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AntennaInFuselage>()
                .HasRequired(a => a.Fuselage)
                .WithMany(f => f.AntennasForFuselage)
                .HasForeignKey(a => a.Id_Fuselage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AntennaInFuselage>()
                .HasRequired(a => a.Antenna)
                .WithMany(an => an.AntennasInFuselage)
                .HasForeignKey(a => a.Id_Antenna)
                .WillCascadeOnDelete(false);
        }
    }
}
