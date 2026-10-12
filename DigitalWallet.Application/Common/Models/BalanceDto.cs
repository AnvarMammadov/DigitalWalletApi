namespace DigitalWallet.Application.Common.Models
{
    public record BalanceDto(
        string WalletNumber,
        decimal Balance,
        string Currency
    );
}
