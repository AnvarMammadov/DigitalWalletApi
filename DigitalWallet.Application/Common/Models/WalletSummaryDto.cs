using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Common.Models
{
    public  record WalletSummaryDto(
    
        string WalletNumber,
        decimal CurrentBalance,
        decimal TotalInflow,
        decimal TotalOutflow,
        int TransactionCount
        );
    
}
