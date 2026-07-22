using System.Collections.Generic;
using System.Reflection.Emit;
using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Persistence
{
    public class DigitalWalletDbContext : DbContext
    {
        public DigitalWalletDbContext(DbContextOptions<DigitalWalletDbContext> options)
            : base(options)
        {
        }

        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DigitalWalletDbContext).Assembly);
        }
    }
}