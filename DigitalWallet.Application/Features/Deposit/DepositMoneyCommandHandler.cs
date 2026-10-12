using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.Exceptions;
using MediatR;

namespace DigitalWallet.Application.Features.Deposit
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, DepositResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepositMoneyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepositResultDto> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Cüzdanı tap
            var wallet = await _unitOfWork.Wallets.GetByWalletNumberAsync(request.WalletNumber, cancellationToken)
                ?? throw new WalletNotFoundException(request.WalletNumber);

            // Addım 2: Domain qaydası — cüzdana pul daxil et (Guard Clause-lar Entity-nin içində işləyəcək)
            wallet.Credit(request.Amount);

            // Addım 3: Transaction yarat (Deposit tipi ilə)
            var transaction = new Transaction(TransactionType.Deposit, request.Amount);
            transaction.MarkAsCompleted();

            // Addım 4: Ledger qeydi — yalnız 1 entry (Credit istiqamətində)
            // Transfer-dən fərqli olaraq, burada yalnız BİR tərəf sistemdədir.
            // Pulun mənbəyi xarici mənbədir (bank kartı, ATM və s.) — "External wallet" konsepti.
            var creditEntry = new LedgerEntry(transaction.Id, wallet.Id, LedgerDirection.Credit, request.Amount);

            // Addım 5: Hamısını yaddaşa əlavə et
            await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.LedgerEntries.AddAsync(creditEntry, cancellationToken);

            // Addım 6: Hamısını BİR YERDƏ bazaya yaz (atomik əməliyyat)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Addım 7: Nəticəni qaytar — yeni balansı da göstər
            return new DepositResultDto(transaction.Id, wallet.Balance, transaction.Status.ToString());
        }
    }
}
