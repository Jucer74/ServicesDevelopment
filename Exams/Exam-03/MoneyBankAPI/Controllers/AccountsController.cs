using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;

namespace MoneyBankService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts([FromQuery] string? accountNumber)
    {
        var result = await _accountService.GetAccountsAsync(accountNumber);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var result = await _accountService.GetAccountByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAccount([FromBody] AccountDto dto)
    {
        var result = await _accountService.CreateAccountAsync(dto);
        return Ok(new { message = "Cuenta creada exitosamente", dto = result });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto dto)
    {
        await _accountService.UpdateAccountAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        await _accountService.DeleteAccountAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/Deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto dto)
    {
        await _accountService.DepositAsync(id, dto);
        return NoContent();
    }

    [HttpPut("{id}/Withdrawal")]
    public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto dto)
    {
        await _accountService.WithdrawAsync(id, dto);
        return NoContent();
    }
}
