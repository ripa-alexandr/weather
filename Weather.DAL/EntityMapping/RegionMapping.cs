
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.DAL.Entities;

namespace Weather.DAL.EntityMapping
{
    public class RegionMapping : EntityTypeConfiguration<RegionEntity>
    {
        public RegionMapping()
        {
            // Table name
            this.ToTable("Regions");

            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.Country)
                .WithMany(t => t.Regions)
                .HasForeignKey(k => k.CountryId);
        }
    }
}
