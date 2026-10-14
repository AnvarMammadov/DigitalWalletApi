using MediatR;
using Microsoft.AspNetCore.Mvc;
using DigitalWallet.Application.Features.CreateWallet;
using DigitalWallet.Application.Features.Deposit;
using DigitalWallet.Application.Features.WithDraw;
using DigitalWallet.Application.Features.GetBalance;
using DigitalWallet.Application.Features.GetTransactions;
using DigitalWallet.Application.Features.GetWalletSummary;
using DigitalWallet.Application.Features.ReconcileWallet;

namespace DigitalWallet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni cüzdan yaradır.
        /// POST /api/wallet
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetBalance), new { walletNumber = result.WalletNumber }, result);
        }

        /// <summary>
        /// Cüzdana pul daxil edir (Deposit).
        /// POST /api/wallet/deposit
        /// </summary>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cüzdandan pul çıxarır (WithDraw).
        /// POST /api/wallet/withdraw
        /// </summary>
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithDraw(WithDrawMoneyCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cüzdanın balansını qaytarır (GetBalance).
        /// GET /api/wallet/{walletNumber}/balance
        /// </summary>
        [HttpGet("{walletNumber}/balance")]
        public async Task<IActionResult> GetBalance(string walletNumber, CancellationToken cancellationToken)
        {
            var query = new GetBalanceQuery(walletNumber);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cüzdanın əməliyyat tarixçəsini (çıxarışını) qaytarır.
        /// GET /api/wallet/{walletNumber}/transactions
        /// </summary>
        [HttpGet("{walletNumber}/transactions")]
        public async Task<IActionResult> GetTransactions(string walletNumber, CancellationToken cancellationToken)
        {
            var query = new GetWalletTransactionsQuery(walletNumber);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpGet("{walletNumber}/summary")] 
        public async Task<IActionResult> GetWalletSummary(string walletNumber, CancellationToken cancellationToken)
        {
            var query=new GetWalletSummaryQuery(walletNumber);
            var result= await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{walletNumber}/reconcile")]
        public async Task<IActionResult> GetReconcile(string walletNumber,CancellationToken cancellationToken)
        {
            var query=new ReconcileWalletQuery(walletNumber);
            var result = await _mediator.Send(query,cancellationToken); 
            return Ok(result);
        }
    }
}
