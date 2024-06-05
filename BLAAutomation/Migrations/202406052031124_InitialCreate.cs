namespace BLAAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AntennaParameters",
                c => new
                    {
                        AntennaMark = c.String(nullable: false, maxLength: 128),
                        FrequencyRange = c.Double(nullable: false),
                        Gain = c.Double(nullable: false),
                        Power = c.Double(nullable: false),
                        Impedance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AntennaMark);
            
            CreateTable(
                "dbo.EquipmentParameters",
                c => new
                    {
                        EquipmentModel = c.String(nullable: false, maxLength: 128),
                        PowerConsumption = c.Double(nullable: false),
                        NoiseImmunity = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Project_ProjectNumber = c.Int(),
                    })
                .PrimaryKey(t => t.EquipmentModel)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectNumber)
                .Index(t => t.Project_ProjectNumber);
            
            CreateTable(
                "dbo.EquipmentPlacementSchemes",
                c => new
                    {
                        SchemeNumber = c.Int(nullable: false, identity: true),
                        EquipmentModel = c.String(nullable: false, maxLength: 128),
                        DeviceModel = c.String(nullable: false, maxLength: 128),
                        CompartmentNumber = c.Int(nullable: false),
                        SchemeDescription = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        UavModel = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SchemeNumber)
                .ForeignKey("dbo.EquipmentParameters", t => t.EquipmentModel)
                .ForeignKey("dbo.UavCompartments", t => t.CompartmentNumber, cascadeDelete: true)
                .ForeignKey("dbo.UavDevices", t => t.DeviceModel)
                .ForeignKey("dbo.UavParameters", t => t.UavModel)
                .Index(t => t.EquipmentModel)
                .Index(t => t.DeviceModel)
                .Index(t => t.CompartmentNumber)
                .Index(t => t.UavModel);
            
            CreateTable(
                "dbo.UavCompartments",
                c => new
                    {
                        CompartmentNumber = c.Int(nullable: false, identity: true),
                        UavModel = c.String(nullable: false, maxLength: 128),
                        LoadCapacity = c.Double(nullable: false),
                        CoordinateX = c.Double(nullable: false),
                        CoordinateY = c.Double(nullable: false),
                        CoordinateZ = c.Double(nullable: false),
                        Length = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Project_ProjectNumber = c.Int(),
                    })
                .PrimaryKey(t => t.CompartmentNumber)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectNumber)
                .ForeignKey("dbo.UavParameters", t => t.UavModel)
                .Index(t => t.UavModel)
                .Index(t => t.Project_ProjectNumber);
            
            CreateTable(
                "dbo.UavParameters",
                c => new
                    {
                        UavModel = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        TotalCompartments = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UavModel);
            
            CreateTable(
                "dbo.UavAntennas",
                c => new
                    {
                        AntennaMark = c.String(nullable: false, maxLength: 128),
                        UavModel = c.String(nullable: false, maxLength: 128),
                        AntennaModel = c.String(),
                        Name = c.String(),
                        CoordinateX = c.Double(nullable: false),
                        CoordinateY = c.Double(nullable: false),
                        CoordinateZ = c.Double(nullable: false),
                        Project_ProjectNumber = c.Int(),
                    })
                .PrimaryKey(t => t.AntennaMark)
                .ForeignKey("dbo.UavParameters", t => t.UavModel)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectNumber)
                .Index(t => t.UavModel)
                .Index(t => t.Project_ProjectNumber);
            
            CreateTable(
                "dbo.LandingSites",
                c => new
                    {
                        LandingSiteNumber = c.Int(nullable: false, identity: true),
                        UavModel = c.String(nullable: false, maxLength: 128),
                        CoordinateX = c.Double(nullable: false),
                        CoordinateY = c.Double(nullable: false),
                        CoordinateZ = c.Double(nullable: false),
                        WeightLimit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LandingSiteNumber)
                .ForeignKey("dbo.UavParameters", t => t.UavModel)
                .Index(t => t.UavModel);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectNumber = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        UavModel = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectNumber)
                .ForeignKey("dbo.UavParameters", t => t.UavModel)
                .Index(t => t.UavModel);
            
            CreateTable(
                "dbo.UavDevices",
                c => new
                    {
                        DeviceModel = c.String(nullable: false, maxLength: 128),
                        Weight = c.Double(nullable: false),
                        NoiseImmunity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceModel);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPlacementSchemes", "UavModel", "dbo.UavParameters");
            DropForeignKey("dbo.EquipmentPlacementSchemes", "DeviceModel", "dbo.UavDevices");
            DropForeignKey("dbo.UavCompartments", "UavModel", "dbo.UavParameters");
            DropForeignKey("dbo.Projects", "UavModel", "dbo.UavParameters");
            DropForeignKey("dbo.UavAntennas", "Project_ProjectNumber", "dbo.Projects");
            DropForeignKey("dbo.EquipmentParameters", "Project_ProjectNumber", "dbo.Projects");
            DropForeignKey("dbo.UavCompartments", "Project_ProjectNumber", "dbo.Projects");
            DropForeignKey("dbo.LandingSites", "UavModel", "dbo.UavParameters");
            DropForeignKey("dbo.UavAntennas", "UavModel", "dbo.UavParameters");
            DropForeignKey("dbo.EquipmentPlacementSchemes", "CompartmentNumber", "dbo.UavCompartments");
            DropForeignKey("dbo.EquipmentPlacementSchemes", "EquipmentModel", "dbo.EquipmentParameters");
            DropIndex("dbo.Projects", new[] { "UavModel" });
            DropIndex("dbo.LandingSites", new[] { "UavModel" });
            DropIndex("dbo.UavAntennas", new[] { "Project_ProjectNumber" });
            DropIndex("dbo.UavAntennas", new[] { "UavModel" });
            DropIndex("dbo.UavCompartments", new[] { "Project_ProjectNumber" });
            DropIndex("dbo.UavCompartments", new[] { "UavModel" });
            DropIndex("dbo.EquipmentPlacementSchemes", new[] { "UavModel" });
            DropIndex("dbo.EquipmentPlacementSchemes", new[] { "CompartmentNumber" });
            DropIndex("dbo.EquipmentPlacementSchemes", new[] { "DeviceModel" });
            DropIndex("dbo.EquipmentPlacementSchemes", new[] { "EquipmentModel" });
            DropIndex("dbo.EquipmentParameters", new[] { "Project_ProjectNumber" });
            DropTable("dbo.UavDevices");
            DropTable("dbo.Projects");
            DropTable("dbo.LandingSites");
            DropTable("dbo.UavAntennas");
            DropTable("dbo.UavParameters");
            DropTable("dbo.UavCompartments");
            DropTable("dbo.EquipmentPlacementSchemes");
            DropTable("dbo.EquipmentParameters");
            DropTable("dbo.AntennaParameters");
        }
    }
}
