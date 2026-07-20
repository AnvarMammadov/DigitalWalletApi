using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Exceptions
{
    public class InvalidTransactionStateException : DomainException
    {
        public TransactionStatus CurrentStatus { get; }
        public TransactionStatus AttemptedStatus { get; }

        public InvalidTransactionStateException(TransactionStatus currentStatus, TransactionStatus attemptedStatus)
            : base($"'{currentStatus}' statuslu əməliyyat '{attemptedStatus}' statusuna keçirilə bilməz. Yalnız 'Pending' statuslu əməliyyat dəyişdirilə bilər.")
        {
            CurrentStatus = currentStatus;
            AttemptedStatus = attemptedStatus;
        }
    }
}
