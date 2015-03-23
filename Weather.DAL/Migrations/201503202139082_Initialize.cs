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
                        Provider = c.Int(nullable: false),
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
                        Provider = c.Int(nullable: false),
                        ProviderName = c.String(),
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
                        Precipitation = c.Int(nullable: false),
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
            
            CreateStoredProcedure(
                "dbo.WeatherData_Insert",
                p => new
                    {
                        Provider = p.Int(),
                        ProviderName = p.String(),
                        DateTime = p.DateTime(),
                        CityId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[WeatherData]([Provider], [ProviderName], [DateTime], [CityId])
                      VALUES (@Provider, @ProviderName, @DateTime, @CityId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[WeatherData]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[WeatherData] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.WeatherData_Update",
                p => new
                    {
                        Id = p.Int(),
                        Provider = p.Int(),
                        ProviderName = p.String(),
                        DateTime = p.DateTime(),
                        CityId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[WeatherData]
                      SET [Provider] = @Provider, [ProviderName] = @ProviderName, [DateTime] = @DateTime, [CityId] = @CityId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.WeatherData_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[WeatherData]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.WeatherDescription_Insert",
                p => new
                    {
                        Id = p.Int(),
                        Cloudy = p.Int(),
                        Precipitation = p.Int(),
                        StrengthPrecipitation = p.Int(),
                        IsFog = p.Boolean(),
                        IsThunderstorm = p.Boolean(),
                        AirTemp = p.Double(),
                        RealFeel = p.Double(),
                        Pressure = p.Double(),
                        WindDirection = p.Int(),
                        WindSpeed = p.Double(),
                        Humidity = p.Double(),
                        ChancePrecipitation = p.Double(),
                    },
                body:
                    @"INSERT [dbo].[WeatherDescriptions]([Id], [Cloudy], [Precipitation], [StrengthPrecipitation], [IsFog], [IsThunderstorm], [AirTemp], [RealFeel], [Pressure], [WindDirection], [WindSpeed], [Humidity], [ChancePrecipitation])
                      VALUES (@Id, @Cloudy, @Precipitation, @StrengthPrecipitation, @IsFog, @IsThunderstorm, @AirTemp, @RealFeel, @Pressure, @WindDirection, @WindSpeed, @Humidity, @ChancePrecipitation)"
            );
            
            CreateStoredProcedure(
                "dbo.WeatherDescription_Update",
                p => new
                    {
                        Id = p.Int(),
                        Cloudy = p.Int(),
                        Precipitation = p.Int(),
                        StrengthPrecipitation = p.Int(),
                        IsFog = p.Boolean(),
                        IsThunderstorm = p.Boolean(),
                        AirTemp = p.Double(),
                        RealFeel = p.Double(),
                        Pressure = p.Double(),
                        WindDirection = p.Int(),
                        WindSpeed = p.Double(),
                        Humidity = p.Double(),
                        ChancePrecipitation = p.Double(),
                    },
                body:
                    @"UPDATE [dbo].[WeatherDescriptions]
                      SET [Cloudy] = @Cloudy, [Precipitation] = @Precipitation, [StrengthPrecipitation] = @StrengthPrecipitation, [IsFog] = @IsFog, [IsThunderstorm] = @IsThunderstorm, [AirTemp] = @AirTemp, [RealFeel] = @RealFeel, [Pressure] = @Pressure, [WindDirection] = @WindDirection, [WindSpeed] = @WindSpeed, [Humidity] = @Humidity, [ChancePrecipitation] = @ChancePrecipitation
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.WeatherDescription_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[WeatherDescriptions]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.WeatherDescription_Delete");
            DropStoredProcedure("dbo.WeatherDescription_Update");
            DropStoredProcedure("dbo.WeatherDescription_Insert");
            DropStoredProcedure("dbo.WeatherData_Delete");
            DropStoredProcedure("dbo.WeatherData_Update");
            DropStoredProcedure("dbo.WeatherData_Insert");
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
