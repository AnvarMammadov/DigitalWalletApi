using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.GetWalletSummary
{
    public  record GetWalletSummaryQuery(
        string WalletNumber
        ):IRequest<WalletSummaryDto>;

}
