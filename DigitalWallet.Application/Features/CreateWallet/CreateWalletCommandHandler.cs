using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using MediatR;

namespace DigitalWallet.Application.Features.CreateWallet
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, CreateWalletResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWalletCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateWalletResultDto> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            // Addım 1: Domain Entity yarat
            // Wallet konstruktoru özü Id yaradır (Guid.NewGuid()) və Balance = 0 qoyur
            var wallet = new Wallet(request.UserId, request.WalletNumber, request.Currency);

            // Addım 2: Anbara əlavə et (hələ bazaya yazılmır, Change Tracker izləyir)
            await _unitOfWork.Wallets.AddAsync(wallet, cancellationToken);

            // Addım 3: Bazaya yaz
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Addım 4: Nəticəni qaytar
            return new CreateWalletResultDto(wallet.Id, wallet.WalletNumber, wallet.Currency);
        }
    }
}
