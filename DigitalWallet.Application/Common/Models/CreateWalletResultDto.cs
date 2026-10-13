namespace DigitalWallet.Application.Common.Models
{
    public record CreateWalletResultDto(
        Guid WalletId,
        string WalletNumber,
        string Currency
    );
}
