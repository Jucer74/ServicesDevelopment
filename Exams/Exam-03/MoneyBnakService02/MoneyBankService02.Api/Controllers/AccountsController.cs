using Microsoft.AspNetCore.Mvc;
using MoneyBankService02.Application.Interfaces;
using MoneyBankService02.Application.Dto;
using MoneyBankService02.Api.Middleware;
using MoneyBankService02.Application.Common;

namespace MoneyBankService02.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? accountNumber)
    {
        var accounts = await _accountService.GetAccountsAsync(accountNumber);
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);
        return account == null ? NotFound() : Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
    {
        var result = await _accountService.CreateAccountAsync(accountDto);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data)
            : BadRequest(new ErrorDetails("ValidationError", new List<string> { result.Message ?? "Datos inválidos" }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AccountDto accountDto)
    {
        if (id != accountDto.Id)
        {
            return BadRequest(new ErrorDetails("IdMismatch", new List<string> { "El ID de la ruta no coincide con el de la cuenta" }));
        }

        var result = await _accountService.UpdateAccountAsync(accountDto);
        return result.IsSuccess ? NoContent() : BadRequest(new ErrorDetails("UpdateError", new List<string> { result.Message ?? "Error al actualizar" }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await _accountService.DeleteAccountAsync(id);
        return result.IsSuccess ? NoContent() : NotFound(new ErrorDetails("NotFound", new List<string> { result.Message ?? "Cuenta no encontrada" }));
    }

    [HttpPut("{id}/deposit")]
    public async Task<IActionResult> DepositFunds(int id, [FromBody] TransactionDto transaction)
    {
        var result = await _accountService.DepositAsync(id, transaction);
        return result.IsSuccess ? NoContent() : BadRequest(new ErrorDetails("TransactionError", new List<string> { result.Message ?? "Error en el depósito" }));
    }

    [HttpPut("{id}/withdraw")]
    public async Task<IActionResult> WithdrawFunds(int id, [FromBody] TransactionDto transaction)
    {
        var result = await _accountService.WithdrawalAsync(id, transaction);
        return result.IsSuccess ? NoContent() : BadRequest(new ErrorDetails("TransactionError", new List<string> { result.Message ?? "Error en el retiro" }));
    }
}
