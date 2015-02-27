namespace Weather.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CityEntities", newName: "City");
            RenameTable(name: "dbo.LinkEntities", newName: "Link");
            RenameTable(name: "dbo.RegionEntities", newName: "Region");
            RenameTable(name: "dbo.CountryEntities", newName: "Country");
            RenameTable(name: "dbo.WorldEntities", newName: "World");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.World", newName: "WorldEntities");
            RenameTable(name: "dbo.Country", newName: "CountryEntities");
            RenameTable(name: "dbo.Region", newName: "RegionEntities");
            RenameTable(name: "dbo.Link", newName: "LinkEntities");
            RenameTable(name: "dbo.City", newName: "CityEntities");
        }
    }
}
