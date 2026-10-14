using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.ReconcileWallet
{
    public record ReconcileWalletQuery(string WalletNumber) : IRequest<ReconcileWalletResultDto>;

}
