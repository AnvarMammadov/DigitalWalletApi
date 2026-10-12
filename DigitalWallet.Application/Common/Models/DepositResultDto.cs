namespace DigitalWallet.Application.Common.Models
{
    public record DepositResultDto(
        Guid TransactionId,
        decimal NewBalance,
        string Status
    );
}
