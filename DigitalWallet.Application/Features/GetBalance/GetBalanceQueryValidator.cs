using FluentValidation;

namespace DigitalWallet.Application.Features.GetBalance
{
    public class GetBalanceQueryValidator : AbstractValidator<GetBalanceQuery>
    {
        public GetBalanceQueryValidator()
        {
            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");
        }
    }
}
