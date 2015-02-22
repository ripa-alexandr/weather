namespace Weather.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEntityPropertyNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cities", newName: "CityEntities");
            RenameTable(name: "dbo.Regions", newName: "RegionEntities");
            RenameTable(name: "dbo.Countries", newName: "CountryEntities");
            RenameTable(name: "dbo.Worlds", newName: "WorldEntities");
            RenameTable(name: "dbo.WeatherDescriptions", newName: "WeatherDescriptionEntities");
            RenameTable(name: "dbo.Links", newName: "LinkEntities");
            AddColumn("dbo.WeatherDescriptionEntities", "Precipitation", c => c.Int(nullable: false));
            AddColumn("dbo.LinkEntities", "Provider", c => c.Int(nullable: false));
            AddColumn("dbo.WeatherData", "Provider", c => c.Int(nullable: false));
            AddColumn("dbo.WeatherData", "ProviderName", c => c.String());
            DropColumn("dbo.WeatherDescriptionEntities", "TypePrecipitation");
            DropColumn("dbo.LinkEntities", "TypeProvider");
            DropColumn("dbo.WeatherData", "TypeProvider");
            DropColumn("dbo.WeatherData", "NameProvider");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WeatherData", "NameProvider", c => c.String());
            AddColumn("dbo.WeatherData", "TypeProvider", c => c.Int(nullable: false));
            AddColumn("dbo.LinkEntities", "TypeProvider", c => c.Int(nullable: false));
            AddColumn("dbo.WeatherDescriptionEntities", "TypePrecipitation", c => c.Int(nullable: false));
            DropColumn("dbo.WeatherData", "ProviderName");
            DropColumn("dbo.WeatherData", "Provider");
            DropColumn("dbo.LinkEntities", "Provider");
            DropColumn("dbo.WeatherDescriptionEntities", "Precipitation");
            RenameTable(name: "dbo.LinkEntities", newName: "Links");
            RenameTable(name: "dbo.WeatherDescriptionEntities", newName: "WeatherDescriptions");
            RenameTable(name: "dbo.WorldEntities", newName: "Worlds");
            RenameTable(name: "dbo.CountryEntities", newName: "Countries");
            RenameTable(name: "dbo.RegionEntities", newName: "Regions");
            RenameTable(name: "dbo.CityEntities", newName: "Cities");
        }
    }
}
