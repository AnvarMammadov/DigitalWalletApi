using Microsoft.EntityFrameworkCore;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Infrastructure.Persistence;

namespace DigitalWallet.Infrastructure.Persistence.Repositories
{
    public class LedgerEntryRepository : ILedgerEntryRepository
    {
        private readonly DigitalWalletDbContext _context;

        public LedgerEntryRepository(DigitalWalletDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LedgerEntry ledgerEntry, CancellationToken cancellationToken = default)
        {
            await _context.LedgerEntries.AddAsync(ledgerEntry, cancellationToken);
        }

        public async Task<IReadOnlyList<LedgerEntry>> GetByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default)
        {
            return await _context.LedgerEntries
                .Where(l => l.WalletId == walletId)
                .ToListAsync(cancellationToken);
        }
    }
}