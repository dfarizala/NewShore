using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewshoreTest.Api.Domain.Entities;

namespace NewshoreTest.Api.Infrastructure.Configuration
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.ToTable("Aircraft");

            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
