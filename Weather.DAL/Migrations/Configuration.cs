
using Weather.Common.Entities;

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

            var world = new World { Name = "Europe" };
            var country = new Country { Name = "Ukraine", World = world };
            var cityKharkov = new City { Name = "Kharkov" };
            var cityChuguev = new City { Name = "Chuguev" };
            var cityIzum = new City { Name = "Izum" };
            var cityKiev = new City { Name = "Kiev" };
            var citySlavutich = new City { Name = "Slavutich" };
            var cityLvov = new City { Name = "Lvov" };
            var cityTruskavec = new City { Name = "Truskavec" };
            var regionKh = new Region
            {
                Name = "Kharkovskaya Oblast",
                Country = country,
                Cities = new[] { cityKharkov, cityChuguev, cityIzum }
            };
            var regionKiev = new Region
            {
                Name = "Kievskaya Oblast",
                Country = country,
                Cities = new[] { cityKiev, citySlavutich }
            };
            var regionLvov = new Region
            {
                Name = "Lvovskaya Oblast",
                Country = country,
                Cities = new[] { cityLvov, cityTruskavec }
            };

            var links = new[]
            {
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-chuhuiv-12830", cityChuguev),
                this.CreateLink(TypeProvider.Sinoptik, "http://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%87%D1%83%D0%B3%D1%83%D0%B5%D0%B2", cityChuguev),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%A7%D1%83%D0%B3%D1%83%D0%B5%D0%B2%D0%B5", cityChuguev),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-kharkiv-5053", cityKharkov),
                this.CreateLink(TypeProvider.Sinoptik, "http://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%85%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2", cityKharkov),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%A5%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2%D0%B5",cityKharkov),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-kyiv-4944/", cityKiev),
                this.CreateLink(TypeProvider.Sinoptik, "https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%BA%D0%B8%D0%B5%D0%B2", cityKiev),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%9A%D0%B8%D0%B5%D0%B2%D0%B5,_%D0%A3%D0%BA%D1%80%D0%B0%D0%B8%D0%BD%D0%B0", cityKiev),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-izium-5070/", cityIzum),
                this.CreateLink(TypeProvider.Sinoptik, "https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B8%D0%B7%D1%8E%D0%BC", cityIzum),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%98%D0%B7%D1%8E%D0%BC%D0%B5", cityIzum),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-lviv-4949/", cityLvov),
                this.CreateLink(TypeProvider.Sinoptik, "https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%BB%D1%8C%D0%B2%D0%BE%D0%B2", cityLvov),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2%D0%BE_%D0%9B%D1%8C%D0%B2%D0%BE%D0%B2%D0%B5,_%D0%9B%D1%8C%D0%B2%D0%BE%D0%B2%D1%81%D0%BA%D0%B0%D1%8F_%D0%BE%D0%B1%D0%BB%D0%B0%D1%81%D1%82%D1%8C", cityLvov),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-truskavetz-11851/", cityTruskavec),
                this.CreateLink(TypeProvider.Sinoptik, "https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%82%D1%80%D1%83%D1%81%D0%BA%D0%B0%D0%B2%D0%B5%D1%86", cityTruskavec),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%A2%D1%80%D1%83%D1%81%D0%BA%D0%B0%D0%B2%D1%86%D0%B5", cityTruskavec),
                this.CreateLink(TypeProvider.Gismeteo, "http://www.gismeteo.ua/weather-slavutich-11308/", citySlavutich),
                this.CreateLink(TypeProvider.Sinoptik, "https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D1%81%D0%BB%D0%B0%D0%B2%D1%83%D1%82%D0%B8%D1%87", citySlavutich),
                this.CreateLink(TypeProvider.Rp5, "http://rp5.ua/%D0%9F%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0_%D0%B2_%D0%A1%D0%BB%D0%B0%D0%B2%D1%83%D1%82%D0%B8%D1%87%D0%B5", citySlavutich),
            };

            context.Regions.Add(regionKh);
            context.Regions.Add(regionKiev);
            context.Regions.Add(regionLvov);
            context.Links.AddRange(links);
        }

        private Link CreateLink(TypeProvider typeProvider, string url, City city)
        {
            return new Link
            {
                TypeProvider = typeProvider,
                Url = url,
                City = city
            };
        }
    }
}
