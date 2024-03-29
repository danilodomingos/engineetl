﻿using EngineETL.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EngineETL.Infrastructure.Data.Mappings
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Template");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PropertyCity).HasMaxLength(200);
            builder.Property(x => x.CityPropertyName).HasMaxLength(100);
            builder.Property(x => x.CityPropertyHabitants).HasMaxLength(100);
            builder.Property(x => x.PropertyNeighborhood).HasMaxLength(100);
            builder.Property(x => x.NeighborhoodPropertyName).HasMaxLength(100);
            builder.Property(x => x.NeighborhoodPropertyHabitants).HasMaxLength(100);


            #region Relationships

            builder.HasOne(x => x.User)
                .WithMany(x => x.ExpectedFormats)
               .HasForeignKey(x => x.UserId);

            #endregion
        }
    }
}
