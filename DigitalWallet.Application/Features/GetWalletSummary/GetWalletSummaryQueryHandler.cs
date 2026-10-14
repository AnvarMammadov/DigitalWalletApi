using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.GetWalletSummary
{
    public class GetWalletSummaryQueryHandler : IRequestHandler<GetWalletSummaryQuery,WalletSummaryDto>
    {
       private readonly IUnitOfWork _unitOfWork;    
        public GetWalletSummaryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WalletSummaryDto> Handle(GetWalletSummaryQuery request,CancellationToken cancellationToken) {
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.WalletNumber);

            var entries = await _unitOfWork.LedgerEntries.GetByWalletIdAsync(wallet.Id, cancellationToken);

            var TotalInflow = entries.Where(e => e.Direction == LedgerDirection.Credit).Sum(e => e.Amount);
            var TotalOutflow = entries.Where(e => e.Direction == LedgerDirection.Debit).Sum(e => e.Amount);
            var TransactionCount = entries.Count;

            return new WalletSummaryDto(wallet.WalletNumber,wallet.Balance,TotalInflow, TotalOutflow, TransactionCount);    


        }



    }
}
