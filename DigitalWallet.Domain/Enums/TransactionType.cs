using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Enums
{
    public enum  TransactionType:byte
    {
        Deposit=1,
        Withdrawal=2,
        Transfer=3
    }
}
