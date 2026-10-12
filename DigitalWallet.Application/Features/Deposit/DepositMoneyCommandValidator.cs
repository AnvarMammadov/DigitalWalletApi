using FluentValidation;

namespace DigitalWallet.Application.Features.Deposit
{
    public class DepositMoneyCommandValidator : AbstractValidator<DepositMoneyCommand>
    {
        public DepositMoneyCommandValidator()
        {
            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Daxil edilən məbləğ sıfırdan böyük olmalıdır.");
        }
    }
}
