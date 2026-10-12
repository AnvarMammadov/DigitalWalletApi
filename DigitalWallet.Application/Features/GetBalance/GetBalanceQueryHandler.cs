using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBalanceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            // Query — sadəcə OXUYUR, heç nə dəyişmir!
            // Burada Transaction, LedgerEntry yaradılmır, SaveChangesAsync çağırılmır.
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.WalletNumber);

            // Entity-ni birbaşa qaytarmırıq — DTO-ya çeviririk
            return new BalanceDto(wallet.WalletNumber, wallet.Balance, wallet.Currency);
        }
    }
}
