using FluentValidation;

namespace DigitalWallet.Application.Features.WithDraw
{
    public class WithDrawMoneyCommandValidator : AbstractValidator<WithDrawMoneyCommand>
    {
        public WithDrawMoneyCommandValidator()
        {
            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Çıxarılacaq məbləğ sıfırdan böyük olmalıdır.");
        }
    }
}
