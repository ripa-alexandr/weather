﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.Common.Entities;

namespace Weather.DAL.EntityMapping
{
    public class CountryMapping : EntityTypeConfiguration<CountryEntity>
    {
        public CountryMapping()
        {
            // Auto generated GUID as primary key
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            this.HasRequired(l => l.World)
                .WithMany(t => t.Countries)
                .Map(m => m.MapKey("WorldId"));
        }
    }
}
