using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballTransfers.Core.Entities;

namespace FootballTransfers.Infrastructure.Data.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransferFee)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.ContractLength)
                .HasMaxLength(20);

            builder.Property(t => t.TransferType)
                .HasConversion<int>();

            builder.HasOne(t => t.Player)
                .WithMany()
                .HasForeignKey(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.FromClub)
                .WithMany(c => c.TransfersFrom)
                .HasForeignKey(t => t.FromClubId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.ToClub)
                .WithMany(c => c.TransfersTo)
                .HasForeignKey(t => t.ToClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.TransferDate);
            builder.HasIndex(t => t.TransferFee);
            builder.HasIndex(t => t.IsConfirmed);
        }
    }
}