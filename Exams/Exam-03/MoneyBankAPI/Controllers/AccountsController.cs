using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    // GET: api/Accounts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts([FromQuery] string? accountNumber)
    {
        if (!string.IsNullOrEmpty(accountNumber))
        {
            var filtered = await _service.FindByAccountNumberAsync(accountNumber);
            return Ok(filtered);
        }

        var accounts = await _service.GetAllAsync();
        return Ok(accounts);
    }

    // GET: api/Accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccount(int id)
    {
        var account = await _service.GetByIdAsync(id);
        return Ok(account);
    }

    // POST: api/Accounts
    [HttpPost]
    public async Task<ActionResult<AccountDto>> PostAccount([FromBody] AccountDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAccount), new { id = created.Id }, created);
    }

    // PUT: api/Accounts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE: api/Accounts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // PUT: api/Accounts/5/Deposit
    [HttpPut("{id}/Deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transaction)
    {
        await _service.DepositAsync(id, transaction);
        return NoContent();
    }

    // PUT: api/Accounts/5/Withdrawal
    [HttpPut("{id}/Withdrawal")]
    public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transaction)
    {
        await _service.WithdrawAsync(id, transaction);
        return NoContent();
    }
}
