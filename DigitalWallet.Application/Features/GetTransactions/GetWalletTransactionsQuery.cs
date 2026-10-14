using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.GetTransactions
{
    public record GetWalletTransactionsQuery(
        string WalletNumber
    ) : IRequest<IReadOnlyList<TransactionHistoryDto>>;
}
