using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.ReconcileWallet
{
    public class ReconcileWalletQueryHandler :IRequestHandler<ReconcileWalletQuery,ReconcileWalletResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;   
        public  ReconcileWalletQueryHandler(IUnitOfWork unitOfWork)
        {
              _unitOfWork = unitOfWork;
        }

        public async Task<ReconcileWalletResultDto> Handle(ReconcileWalletQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber,cancellationToken)??
                throw  new WalletNotFoundException(request.WalletNumber);
            var entries = await _unitOfWork.LedgerEntries.GetByWalletIdAsync(wallet.Id, cancellationToken);
            var calculatedBalance = entries.Where(e => e.Direction == LedgerDirection.Credit).
                Sum(e => e.Amount) - entries.Where(e => e.Direction == LedgerDirection.Debit).Sum(e => e.Amount);
            var isBalanced=wallet.Balance==calculatedBalance;
            var discrepancy = wallet.Balance - calculatedBalance;
            return new ReconcileWalletResultDto(wallet.WalletNumber,wallet.Balance,calculatedBalance, isBalanced,discrepancy);
        }
    }
}
