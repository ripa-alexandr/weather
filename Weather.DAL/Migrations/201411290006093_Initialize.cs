namespace Weather.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        TypeProvider = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WorldId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Worlds", t => t.WorldId, cascadeDelete: true)
                .Index(t => t.WorldId);
            
            CreateTable(
                "dbo.Worlds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeatherData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeProvider = c.Int(nullable: false),
                        NameProvider = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.WeatherDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Cloudy = c.Int(nullable: false),
                        TypePrecipitation = c.Int(nullable: false),
                        StrengthPrecipitation = c.Int(nullable: false),
                        IsFog = c.Boolean(nullable: false),
                        IsThunderstorm = c.Boolean(nullable: false),
                        AirTemp = c.Double(nullable: false),
                        RealFeel = c.Double(),
                        Pressure = c.Double(nullable: false),
                        WindDirection = c.Int(nullable: false),
                        WindSpeed = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                        ChancePrecipitation = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WeatherData", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherDescriptions", "Id", "dbo.WeatherData");
            DropForeignKey("dbo.WeatherData", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "WorldId", "dbo.Worlds");
            DropForeignKey("dbo.Links", "CityId", "dbo.Cities");
            DropIndex("dbo.WeatherDescriptions", new[] { "Id" });
            DropIndex("dbo.WeatherData", new[] { "CityId" });
            DropIndex("dbo.Countries", new[] { "WorldId" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Links", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "RegionId" });
            DropTable("dbo.WeatherDescriptions");
            DropTable("dbo.WeatherData");
            DropTable("dbo.Worlds");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Links");
            DropTable("dbo.Cities");
        }
    }
}
