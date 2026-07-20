using System;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;

namespace DigitalWallet.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public void MarkAsCompleted()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidTransactionStateException(Status,TransactionStatus.Completed);
            }

            Status = TransactionStatus.Completed;
        }

        public void MarkAsFailed()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidTransactionStateException(Status,TransactionStatus.Failed);
            }

            Status = TransactionStatus.Failed;
        }

        public Transaction(TransactionType type, decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException(amount);
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