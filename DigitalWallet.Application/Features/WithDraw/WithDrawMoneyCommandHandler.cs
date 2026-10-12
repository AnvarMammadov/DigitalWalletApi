using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.WithDraw
{
    public class WithDrawMoneyCommandHandler : IRequestHandler<WithDrawMoneyCommand, WithDrawResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public WithDrawMoneyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WithDrawResultDto> Handle(WithDrawMoneyCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Cüzdanı tap
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.WalletNumber);

            // Addım 2: Domain qaydası — cüzdandan pul çıx
            // Debit() metodu 2 Guard Clause yoxlayır:
            //   1) Məbləğ > 0 olmalıdır
            //   2) Balans >= məbləğ olmalıdır (InsufficientFundsException ata bilər!)
            wallet.Debit(request.Amount);

            // Addım 3: Transaction yarat (Withdrawal tipi ilə)
            var transaction = new Transaction(TransactionType.Withdrawal, request.Amount);
            transaction.MarkAsCompleted();

            // Addım 4: Ledger qeydi — yalnız 1 entry (Debit istiqamətində)
            // Deposit-in əksi: pul sistemdən ÇIXIR, xarici hədəfə gedir (bank kartı, ATM).
            var debitEntry = new LedgerEntry(transaction.Id, wallet.Id, LedgerDirection.Debit, request.Amount);

            // Addım 5: Hamısını yaddaşa əlavə et
            await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.LedgerEntries.AddAsync(debitEntry, cancellationToken);

            // Addım 6: Hamısını BİR YERDƏ bazaya yaz
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Addım 7: Nəticəni qaytar
            return new WithDrawResultDto(transaction.Id, wallet.Balance, transaction.Status.ToString());
        }
    }
}
