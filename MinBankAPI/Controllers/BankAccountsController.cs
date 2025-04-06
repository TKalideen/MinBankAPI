using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinBankAPI.Models;
using MinBankAPI.Repositories;

namespace MinBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountsController : Controller
    {

        private readonly IBankAccountRepository _repository;

        public BankAccountsController(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{accountHolderName}")]
        public async Task<ActionResult<IEnumerable<BankAccounts>>> GetAccounts(string accountHolderName)
        {
            var accounts = await _repository.GetBankAccountsAsync(accountHolderName);
            return Ok(accounts);
        }

        [HttpGet("account/{accountNumber}")]
        public async Task<ActionResult<BankAccounts>> GetAccount(string accountNumber)
        {
            var account = await _repository.GetBankAccountAsync(accountNumber);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] WithdrawFromAccount withdrawal)
        {
            var success = await _repository.WithdrawalAsync(withdrawal.accountNumber, withdrawal.Amount);
            if (!success) return BadRequest("Insufficient balance or invalid account.");
            return Ok("Withdrawal successful.");
        }

        // GET: BankAccountsController
        public ActionResult Index()
        {
            return View();
        }
    }
}
