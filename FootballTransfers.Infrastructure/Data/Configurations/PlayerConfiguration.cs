using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballTransfers.Core.Entities;

namespace FootballTransfers.Infrastructure.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Nationality)
                .HasMaxLength(50);

            builder.Property(p => p.MarketValue)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Position)
                .HasConversion<int>();

            builder.HasOne(p => p.CurrentClub)
                .WithMany(c => c.CurrentPlayers)
                .HasForeignKey(p => p.CurrentClubId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Agent)
                .WithMany(a => a.Players)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(p => new { p.FirstName, p.LastName });
            builder.HasIndex(p => p.Nationality);
        }
    }
}