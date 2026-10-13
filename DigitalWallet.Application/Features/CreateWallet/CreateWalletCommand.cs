using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.CreateWallet
{
    public record CreateWalletCommand(
        Guid UserId,
        string WalletNumber,
        string Currency
    ) : IRequest<CreateWalletResultDto>;
}
