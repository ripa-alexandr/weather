
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

            // StoredProcedures
            this.MapToStoredProcedures(s => s
                .Update(u => u.HasName("WeatherDescription_Update"))
                .Delete(d => d.HasName("WeatherDescription_Delete"))
                .Insert(d => d.HasName("WeatherDescription_Insert")));
        }
    }
}
