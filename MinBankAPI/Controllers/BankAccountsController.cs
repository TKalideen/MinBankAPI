using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinBankAPI.Interfaces;
using MinBankAPI.Models;

namespace MinBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var (success, message) = await _repository.WithdrawalAsync(withdrawal.accountNumber, withdrawal.Amount);

            if (!success)
                return BadRequest(message);  

            return Ok(message);  
        }
    }
}
