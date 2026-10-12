using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.Deposit
{
    public record DepositMoneyCommand(
        string WalletNumber,
        decimal Amount
    ) : IRequest<DepositResultDto>;
}
