
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.DAL.Entities;

namespace Weather.DAL.EntityMapping
{
    public class CountryMapping : EntityTypeConfiguration<CountryEntity>
    {
        public CountryMapping()
        {
            // Table name
            this.ToTable("Countries");

            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.World)
                .WithMany(t => t.Countries)
                .HasForeignKey(k => k.WorldId);
        }
    }
}
