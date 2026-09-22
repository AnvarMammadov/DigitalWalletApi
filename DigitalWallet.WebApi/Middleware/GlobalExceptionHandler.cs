using DigitalWallet.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebApi.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                WalletNotFoundException => (StatusCodes.Status404NotFound, "Cüzdan tapılmadı"),
                InsufficientFundsException => (StatusCodes.Status422UnprocessableEntity, "Kifayət qədər vəsait yoxdur"),
                DomainException => (StatusCodes.Status422UnprocessableEntity, "Biznes qaydası pozuldu"),
                FluentValidation.ValidationException => (StatusCodes.Status400BadRequest, "Doğrulama xətası"),
                _ => (StatusCodes.Status500InternalServerError, "Server xətası")
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message
            }, cancellationToken);

            return true;
        }
    }
}