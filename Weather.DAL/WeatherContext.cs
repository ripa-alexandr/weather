
using System.Data.Entity;

using Weather.DAL.EntityMapping;
using Weather.Data.Entities;

namespace Weather.DAL
{
    public class WeatherContext : DbContext
    {
        public WeatherContext()
            : this("Weather")
        {
        }

        public WeatherContext(string name)
            : base(name)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<WeatherData> WeatherData { get; set; }

        public DbSet<WeatherDescription> WeatherDescription { get; set; }

        public DbSet<World> Worlds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMapping());
            modelBuilder.Configurations.Add(new CountryMapping());
            modelBuilder.Configurations.Add(new LinkMapping());
            modelBuilder.Configurations.Add(new RegionMapping());
            modelBuilder.Configurations.Add(new WeatherDataMapping());
            modelBuilder.Configurations.Add(new WorldMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
