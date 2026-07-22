using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.Transfer
{
    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, TransferResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferMoneyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TransferResultDto> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var fromWallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.FromWalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.FromWalletNumber);

            var toWallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.ToWalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.ToWalletNumber);

            fromWallet.Debit(request.Amount);
            toWallet.Credit(request.Amount);

            // TODO: Audit trail üçün, Transaction-ı Pending statusda əvvəlcədən saxlayıb,
            // uğursuz cəhdlərdə MarkAsFailed() ilə yeniləmək gələcəkdə əlavə oluna bilər.
            var transaction = new Transaction(TransactionType.Transfer, request.Amount);
            transaction.MarkAsCompleted();

            var debitEntry = new LedgerEntry(transaction.Id, fromWallet.Id, LedgerDirection.Debit, request.Amount);
            var creditEntry = new LedgerEntry(transaction.Id, toWallet.Id, LedgerDirection.Credit, request.Amount);

            await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.LedgerEntries.AddAsync(debitEntry, cancellationToken);
            await _unitOfWork.LedgerEntries.AddAsync(creditEntry, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TransferResultDto(transaction.Id, transaction.Status.ToString());
        }
    }
}