using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.WithDraw
{
    public record WithDrawMoneyCommand(
        string WalletNumber,
        decimal Amount
    ) : IRequest<WithDrawResultDto>;
}
