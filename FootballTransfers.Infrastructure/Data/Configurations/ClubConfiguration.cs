using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballTransfers.Core.Entities;

namespace FootballTransfers.Infrastructure.Data.Configurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Country)
                .HasMaxLength(50);

            builder.Property(c => c.League)
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .HasMaxLength(50);

            builder.Property(c => c.Stadium)
                .HasMaxLength(100);

            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasIndex(c => c.Country);
            builder.HasIndex(c => c.League);
        }
    }
}