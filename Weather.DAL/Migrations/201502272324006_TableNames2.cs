namespace Weather.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNames2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WeatherDescriptionEntities", newName: "WeatherDescription");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.WeatherDescription", newName: "WeatherDescriptionEntities");
        }
    }
}
