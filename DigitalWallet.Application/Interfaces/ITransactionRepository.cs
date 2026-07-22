using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = DigitalWallet.Domain.Entities.Transaction;

namespace DigitalWallet.Application.Interfaces
{
    public interface  ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken=default);
    }
}
