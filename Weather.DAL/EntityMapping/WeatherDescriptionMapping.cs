
using System.Data.Entity.ModelConfiguration;

using Weather.Common.Entities;

namespace Weather.DAL.EntityMapping
{
    public class WeatherDescriptionMapping : EntityTypeConfiguration<WeatherDescriptionEntity>
    {
        public WeatherDescriptionMapping()
        {
            // Table name
            this.ToTable("WeatherDescriptions");
        }
    }
}
