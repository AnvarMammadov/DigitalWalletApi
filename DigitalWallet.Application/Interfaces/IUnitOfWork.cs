using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IWalletRepository Wallets { get; }
        ITransactionRepository Transactions { get; }    
        ILedgerEntryRepository LedgerEntries { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
