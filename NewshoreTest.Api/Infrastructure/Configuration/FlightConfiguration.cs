using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewshoreTest.Api.Domain.Entities;

namespace NewshoreTest.Api.Infrastructure.Configuration
{
    public class FlightConfiguration: IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.ToTable("Flight");

            builder.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false);
            builder.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false);
        }
    }
}
