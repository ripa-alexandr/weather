
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Data.Entities;

namespace Weather.DAL.EntityMapping
{
    public class WorldMapping : EntityTypeConfiguration<World>
    {
        public WorldMapping()
        {
            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
