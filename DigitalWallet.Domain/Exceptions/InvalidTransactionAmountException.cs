using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Exceptions
{
    public class InvalidTransactionAmountException:DomainException
    {
        public decimal AttemptedAmount { get; }

        public InvalidTransactionAmountException(decimal attemptedAmount)
            : base($"Əməliyyat məbləği sıfırdan böyük olmalıdır. Göndərilən: {attemptedAmount}")
        {
            AttemptedAmount = attemptedAmount;
        }
    }
}
