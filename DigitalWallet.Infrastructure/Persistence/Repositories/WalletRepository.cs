using Microsoft.EntityFrameworkCore;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Infrastructure.Persistence;

namespace DigitalWallet.Infrastructure.Persistence.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DigitalWalletDbContext _context;

        public WalletRepository(DigitalWalletDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<Wallet?> GetByWalletNumberAsync(string walletNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.WalletNumber == walletNumber, cancellationToken);
        }

        public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            await _context.Wallets.AddAsync(wallet, cancellationToken);
        }
    }
}