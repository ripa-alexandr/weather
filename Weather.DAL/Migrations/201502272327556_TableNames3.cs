namespace Weather.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNames3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.City", newName: "Cities");
            RenameTable(name: "dbo.Link", newName: "Links");
            RenameTable(name: "dbo.Region", newName: "Regions");
            RenameTable(name: "dbo.Country", newName: "Countries");
            RenameTable(name: "dbo.World", newName: "Worlds");
            RenameTable(name: "dbo.WeatherDescription", newName: "WeatherDescriptions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.WeatherDescriptions", newName: "WeatherDescription");
            RenameTable(name: "dbo.Worlds", newName: "World");
            RenameTable(name: "dbo.Countries", newName: "Country");
            RenameTable(name: "dbo.Regions", newName: "Region");
            RenameTable(name: "dbo.Links", newName: "Link");
            RenameTable(name: "dbo.Cities", newName: "City");
        }
    }
}
