using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        // GET: api/accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var account = await _accountService.GetAccountById(id);
            return Ok(account);
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            var created = await _accountService.CreateAccount(account);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Account account)
        {
            var updated = await _accountService.UpdateAccount(id, account);
            return Ok(updated);
        }

        // DELETE: api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteAccount(id);
            return NoContent();
        }

        // POST: api/accounts/5/deposit
        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto request)
        {
            var updated = await _accountService.DepositAsync(id, request.ValueAmount);
            return Ok(updated);
        }

        // POST: api/accounts/5/withdraw
        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto request)
        {
            var updated = await _accountService.WithdrawAsync(id, request.ValueAmount);
            return Ok(updated);
        }
    }
}
