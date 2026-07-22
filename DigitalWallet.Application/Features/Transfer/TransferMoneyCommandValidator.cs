using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DigitalWallet.Application.Features.Transfer
{
    public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
    {
        public TransferMoneyCommandValidator()
        {
            RuleFor(x => x.FromWalletNumber)
                .NotEmpty().WithMessage("Göndərən cüzdan nömrəsi boş ola bilməz.");

            RuleFor(x => x.ToWalletNumber)
                .NotEmpty().WithMessage("Qəbul edən cüzdan nömrəsi boş ola bilməz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Məbləğ sıfırdan böyük olmalıdır.");

            RuleFor(x => x)
                .Must(x => x.FromWalletNumber != x.ToWalletNumber)
                .WithMessage("Eyni cüzdana köçürmə edilə bilməz.");
        }
    }
}
