using System;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;

namespace DigitalWallet.Domain.Entities
{
    public class LedgerEntry
    {
        public Guid Id { get; private set; }
        public Guid TransactionId { get; private set; }
        public Guid WalletId { get; private set; }
        public LedgerDirection Direction { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public LedgerEntry(Guid transactionId, Guid walletId, LedgerDirection direction, decimal amount)
        {
            if (transactionId == Guid.Empty)
            {
                throw new ArgumentException("TransactionId boş ola bilməz.", nameof(transactionId));
            }

            if (walletId == Guid.Empty)
            {
                throw new ArgumentException("WalletId boş ola bilməz.", nameof(walletId));
            }

            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException(amount);
            }

            Id = Guid.NewGuid();
            TransactionId = transactionId;
            WalletId = walletId;
            Direction = direction;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }

        private LedgerEntry() { }
    }
}