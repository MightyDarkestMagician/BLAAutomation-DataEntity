using System.Data.Entity;

namespace BLAAutomation.Models
{
    public class UavContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UavParameters> UavParameters { get; set; }
        public DbSet<UavCompartment> UavCompartments { get; set; }
        public DbSet<EquipmentParameters> EquipmentParameters { get; set; }
        public DbSet<EquipmentPlacementScheme> EquipmentPlacementSchemes { get; set; }
        public DbSet<AntennaParameters> AntennaParameters { get; set; }
        public DbSet<UavAntenna> UavAntennas { get; set; }
        public DbSet<LandingSites> LandingSites { get; set; }
        public DbSet<UavDevice> UavDevices { get; set; }

        public UavContext() : base("name=UavContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasRequired(p => p.UavParameters)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UavModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<UavCompartment>()
                .HasRequired(c => c.UavParameters)
                .WithMany(u => u.Compartments)
                .HasForeignKey(c => c.UavModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.UavParameters)
                .WithMany(u => u.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.UavModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.EquipmentParameters)
                .WithMany(e => e.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.EquipmentModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<EquipmentPlacementScheme>()
                .HasRequired(e => e.UavDevice)
                .WithMany(d => d.EquipmentPlacementSchemes)
                .HasForeignKey(e => e.DeviceModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<UavAntenna>()
                .HasRequired(a => a.UavParameters)
                .WithMany(u => u.Antennas)
                .HasForeignKey(a => a.UavModel)
                .WillCascadeOnDelete(false);  // Изменение

            modelBuilder.Entity<LandingSites>()
                .HasRequired(l => l.UavParameters)
                .WithMany(u => u.LandingSites)
                .HasForeignKey(l => l.UavModel)
                .WillCascadeOnDelete(false);  // Изменение
        }
    }
}
