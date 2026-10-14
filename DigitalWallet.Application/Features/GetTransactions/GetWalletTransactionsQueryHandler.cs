using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.GetTransactions
{
    public class GetWalletTransactionsQueryHandler 
        : IRequestHandler<GetWalletTransactionsQuery, IReadOnlyList<TransactionHistoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<TransactionHistoryDto>> Handle(
            GetWalletTransactionsQuery request, 
            CancellationToken cancellationToken)
        {
            // Addım 1: Cüzdanın mövcudluğunu yoxla
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.WalletNumber);

            // Addım 2: Həmin cüzdana aid bütün mühasibat (Ledger) qeydlərini gətir
            var entries = await _unitOfWork.LedgerEntries.GetByWalletIdAsync(wallet.Id, cancellationToken);

            // Addım 3: Ən yenidən ən köhnəyə doğru sırala və DTO-ya çevir
            var result = entries
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new TransactionHistoryDto(
                    x.TransactionId,
                    x.Amount,
                    x.Direction.ToString(),
                    x.CreatedAt
                ))
                .ToList();

            return result;
        }
    }
}
