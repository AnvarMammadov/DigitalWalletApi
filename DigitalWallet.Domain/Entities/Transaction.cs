using System;
using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Transaction(TransactionType type, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Əməliyyat məbləği sıfırdan böyük olmalıdır.", nameof(amount));
            }

            Id = Guid.NewGuid();
            Type = type;
            Amount = amount;
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        private Transaction() { }
    }
}