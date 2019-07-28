using EngineETL.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngineETL.Infrastructure.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Login).HasMaxLength(100);
            builder.Property(x => x.Password);
            builder.Property(x => x.LastAccess);


            #region Relationships

            builder.HasMany(x => x.ExpectedFormats).WithOne();

            #endregion

        }
    }
}
