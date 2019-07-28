using EngineETL.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngineETL.Infrastructure.Data.Mappings
{
    public class ExpectedFormatMap : IEntityTypeConfiguration<ExpectedFormat>
    {
        public void Configure(EntityTypeBuilder<ExpectedFormat> builder)
        {
            builder.ToTable("ExpectedFormat");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PropertyCity).HasMaxLength(200);
            builder.Property(x => x.CityPropertyName).HasMaxLength(100);
            builder.Property(x => x.CityPropertyHabitants).HasMaxLength(100);
            builder.Property(x => x.PropertyNeighborhood).HasMaxLength(100);
            builder.Property(x => x.NeighborhoodPropertyName).HasMaxLength(100);
            builder.Property(x => x.NeighborhoodPropertyHabitants).HasMaxLength(100);
        }
    }
}
