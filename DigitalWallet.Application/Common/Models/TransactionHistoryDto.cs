namespace DigitalWallet.Application.Common.Models
{
    public record TransactionHistoryDto(
        Guid TransactionId,
        decimal Amount,
        string Direction,
        DateTime CreatedAt
    );
}
