using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.Transfer
{
    public record TransferMoneyCommand(
        string FromWalletNumber,
        string ToWalletNumber,
        decimal Amount
    ) : IRequest<TransferResultDto>;
}
