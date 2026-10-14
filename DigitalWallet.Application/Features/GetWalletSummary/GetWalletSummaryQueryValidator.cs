using FluentValidation;

namespace DigitalWallet.Application.Features.GetWalletSummary
{
    public class GetWalletSummaryQueryValidator : AbstractValidator<GetWalletSummaryQuery>
    {
        public GetWalletSummaryQueryValidator() {
            RuleFor(x => x.WalletNumber)
                .NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");
        }
    }
}
