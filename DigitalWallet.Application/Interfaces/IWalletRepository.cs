using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
        Task<Wallet?> GetByWalletNumberAsync(string walletNumber,CancellationToken cancellationToken=default);
        Task AddAsync(Wallet wallet, CancellationToken cancellationToken=default);
    }
}
