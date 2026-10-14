using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Common.Models
{
    public record ReconcileWalletResultDto(
        string WalletNumber,
        decimal CurrentBalance,
        decimal LedgerCalculatedBalance,
        bool IsBalanced,
        decimal Discrepancy);
  
}
