using FluentValidation;

namespace DigitalWallet.Application.Features.GetTransactions
{
    public class GetWalletTransactionsQueryValidator : AbstractValidator<GetWalletTransactionsQuery>
    {
        public GetWalletTransactionsQueryValidator()
        {
            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");
        }
    }
}
