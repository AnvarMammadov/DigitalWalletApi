using DigitalWallet.Application.Interfaces;
using DigitalWallet.Infrastructure.Persistence.Repositories;

namespace DigitalWallet.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DigitalWalletDbContext _context;

        public IWalletRepository Wallets { get; }
        public ITransactionRepository Transactions { get; }
        public ILedgerEntryRepository LedgerEntries { get; }

        public UnitOfWork(DigitalWalletDbContext context)
        {
            _context = context;
            Wallets = new WalletRepository(_context);
            Transactions = new TransactionRepository(_context);
            LedgerEntries = new LedgerEntryRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}