using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .HasColumnType("numeric(18,2)");

            builder.Property(t => t.Type)
                .HasConversion<byte>();

            builder.Property(t => t.Status)
                .HasConversion<byte>();
        }
    }
}