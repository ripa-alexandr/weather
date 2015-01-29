
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Data.Entities;

namespace Weather.DAL.EntityMapping
{
    public class WeatherDataMapping : EntityTypeConfiguration<WeatherData>
    {
        public WeatherDataMapping()
        {
            // Table name
            this.ToTable("WeatherData");
            
            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            // Relationships
            this.HasRequired(l => l.City)
                .WithMany(t => t.WeatherData)
                .HasForeignKey(k => k.CityId);

            this.HasRequired(wd => wd.WeatherDescription)
                .WithRequiredPrincipal(wd => wd.WeatherData);
        }
    }
}
