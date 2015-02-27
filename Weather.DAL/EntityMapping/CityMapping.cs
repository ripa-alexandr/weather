
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Common.Entities;

namespace Weather.DAL.EntityMapping
{
    public class CityMapping : EntityTypeConfiguration<CityEntity>
    {
        public CityMapping()
        {
            // Table name
            this.ToTable("Cities");

            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.Region)
                .WithMany(t => t.Cities)
                .Map(m => m.MapKey("RegionId"));
        }
    }
}
