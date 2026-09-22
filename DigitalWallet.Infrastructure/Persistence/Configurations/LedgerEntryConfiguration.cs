using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Infrastructure.Persistence.Configurations
{
    public class LedgerEntryConfiguration : IEntityTypeConfiguration<LedgerEntry>
    {
        public void Configure(EntityTypeBuilder<LedgerEntry> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Amount)
                .HasColumnType("numeric(18,2)");

            builder.Property(l => l.Direction)
                .HasConversion<byte>();

            builder.HasIndex(l => l.WalletId);
            builder.HasIndex(l => l.TransactionId);
        }
    }
}