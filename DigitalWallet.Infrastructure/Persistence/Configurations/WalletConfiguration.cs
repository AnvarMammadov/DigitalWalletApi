using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Infrastructure.Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.WalletNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(w => w.WalletNumber)
                .IsUnique();

            builder.Property(w => w.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(w => w.Balance)
                .HasColumnType("numeric(18,2)");

            //Shadow Property (RowVersion)
            builder.Property<uint>("xmin")
                .HasColumnName("xmin")
                .HasColumnType("xid")
                .IsRowVersion();
        }
    }
}