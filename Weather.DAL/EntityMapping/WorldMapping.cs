
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Common.Entities;

namespace Weather.DAL.EntityMapping
{
    public class WorldMapping : EntityTypeConfiguration<WorldEntity>
    {
        public WorldMapping()
        {
            // Table name
            this.ToTable("Worlds");

            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
