using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewshoreTest.Api.Domain.Entities;

namespace NewshoreTest.Api.Infrastructure.Configuration
{
    public class CarrierConfiguration : IEntityTypeConfiguration<Carrier>
    {
        public void Configure(EntityTypeBuilder<Carrier> builder)
        {
            builder.HasKey(e => e.Id);            
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.ToTable("Carrier");

            builder.Property(e => e.CarrierName)
                .HasMaxLength(500)
                .IsUnicode(false);
            builder.Property(e => e.CarrierCode)
                .HasMaxLength(3)
                .IsUnicode(false);
        }
    }
}
