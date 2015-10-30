
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Weather.DAL.Entities;
using Weather.Common.Enums;

namespace Weather.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WeatherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeatherContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Links.xml");
            var doc = XDocument.Load(filePath);
            var worlds = doc.Root.Descendants("world").Select(this.CreateWorld);
            context.Worlds.AddRange(worlds);
        }

        private WorldEntity CreateWorld(XElement element)
        {
            return new WorldEntity
            {
                Name = element.Attribute("name").Value,
                Countries = element.Descendants("country").Select(this.CreateCountry).ToList()
            };
        }

        private CountryEntity CreateCountry(XElement element)
        {
            return new CountryEntity
            {
                Name = element.Attribute("name").Value,
                Regions = element.Descendants("region").Select(this.CreateRegion).ToList()
            };
        }

        private RegionEntity CreateRegion(XElement element)
        {
            return new RegionEntity
            {
                Name = element.Attribute("name").Value,
                Cities = element.Descendants("city").Select(this.CreateCity).ToList()
            };
        }

        private CityEntity CreateCity(XElement element)
        {
            return new CityEntity
            {
                Name = element.Attribute("name").Value,
                Links = element.Descendants("link").Select(this.CreateLink).ToList()
            };
        }

        private LinkEntity CreateLink(XElement element)
        {
            return new LinkEntity
            {
                Provider = (Provider)int.Parse(element.Attribute("type").Value),
                Url = element.Attribute("url").Value
            };
        }
    }
}
