
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Common.Entities;

namespace Weather.DAL.EntityMapping
{
    public class RegionMapping : EntityTypeConfiguration<Region>
    {
        public RegionMapping()
        {
            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.Country)
                .WithMany(t => t.Regions)
                .Map(m => m.MapKey("CountryId"));
        }
    }
}
