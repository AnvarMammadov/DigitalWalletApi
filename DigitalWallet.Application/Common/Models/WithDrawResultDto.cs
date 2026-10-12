namespace DigitalWallet.Application.Common.Models
{
    public record WithDrawResultDto(
        Guid TransactionId,
        decimal NewBalance,
        string Status
    );
}
