﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Weather.DAL.Entities;

namespace Weather.DAL.EntityMapping
{
    public class LinkMapping : EntityTypeConfiguration<LinkEntity>
    {
        public LinkMapping()
        {
            // Table name
            this.ToTable("Links");

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
