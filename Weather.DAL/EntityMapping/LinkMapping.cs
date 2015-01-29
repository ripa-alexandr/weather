
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Data.Entities;

namespace Weather.DAL.EntityMapping
{
    public class LinkMapping : EntityTypeConfiguration<Link>
    {
        public LinkMapping()
        {
            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.City)
                .WithMany(t => t.Links)
                .HasForeignKey(k => k.CityId)
                .WillCascadeOnDelete(false);
        }
    }
}
