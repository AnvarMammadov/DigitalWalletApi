using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Features.GetBalance
{
    // DİQQƏT: Bu IRequest-dir, ICommand deyil!
    // GetBalance sistemi DƏYİŞMİR — yalnız OXUYUR. Bu, CQRS-də "Query" adlanır.
    // Amma MediatR-da Query üçün ayrı interface yoxdur, eyni IRequest istifadə olunur.
    public record GetBalanceQuery(
        string WalletNumber
    ) : IRequest<BalanceDto>;
}
