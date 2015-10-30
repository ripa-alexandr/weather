
using System.Data.Entity;

using Weather.DAL.Entities;
using Weather.DAL.EntityMapping;

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

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<CountryEntity> Countries { get; set; }

        public DbSet<LinkEntity> Links { get; set; }

        public DbSet<RegionEntity> Regions { get; set; }

        public DbSet<WeatherDataEntity> WeatherData { get; set; }

        public DbSet<WorldEntity> Worlds { get; set; }

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
