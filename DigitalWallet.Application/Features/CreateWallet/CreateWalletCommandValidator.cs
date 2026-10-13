using FluentValidation;

namespace DigitalWallet.Application.Features.CreateWallet
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("İstifadəçi ID-si boş ola bilməz.");

            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.")
                .MaximumLength(50).WithMessage("Cüzdan nömrəsi 50 simvoldan uzun ola bilməz.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Valyuta boş ola bilməz.")
                .Length(3).WithMessage("Valyuta kodu 3 hərf olmalıdır (məsələn: AZN, USD, EUR).");
        }
    }
}
