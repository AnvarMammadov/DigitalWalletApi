using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Exceptions
{
    public  class InsufficientFundsException : DomainException
    {
        public Guid WalletId { get; }
        public decimal RequestedAmount { get; }
        public decimal AvailableBalance { get; }

        public InsufficientFundsException(Guid walletId, decimal requestedAmount, decimal availableBalance)
            : base($"Cüzdanda ({walletId}) kifayət qədər vəsait yoxdur. Tələb olunan: {requestedAmount}, mövcud balans: {availableBalance}")
        {
            WalletId = walletId;
            RequestedAmount = requestedAmount;
            AvailableBalance = availableBalance;
        }
    }
}
