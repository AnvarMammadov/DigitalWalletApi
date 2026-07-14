using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Enums
{
    public enum TransactionStatus:byte
    {
        Pending=1,
        Processing=2,
        Completed=3,
        Failed=4
    }
}
