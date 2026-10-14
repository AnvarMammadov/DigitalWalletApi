using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DigitalWallet.Application.Features.ReconcileWallet
{
    public class ReconcileWalletQueryValidator : AbstractValidator<ReconcileWalletQuery>
    {
        public ReconcileWalletQueryValidator()
        {
            RuleFor(x => x.WalletNumber).NotEmpty().WithMessage("Cüzdan nömrəsi boş ola bilməz.");
        }
    }
}
