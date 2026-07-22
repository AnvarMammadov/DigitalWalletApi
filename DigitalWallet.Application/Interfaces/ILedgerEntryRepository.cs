using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Interfaces
{
    public  interface  ILedgerEntryRepository
    {
        Task AddAsync(LedgerEntry ledgerEntry , CancellationToken cancellationToken=default);
        Task<IReadOnlyList<LedgerEntry>> GetByWalletIdAsync(Guid walletId, CancellationToken cancellationToken=default);
    }
}
