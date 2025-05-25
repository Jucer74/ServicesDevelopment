using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dtos;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;

namespace MoneyBankService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _svc;
    public AccountsController(IAccountService svc) => _svc = svc;

    // Devuelve 400 Bad Request -> { errorType: "Bad Request", errors: [...] }
    private IActionResult BadRequestError(params string[] mensajes) =>
        BadRequest(new { errorType = "Bad Request", errors = mensajes });

    // Devuelve 404 Not Found -> { errorType: "Not Found", errors: [...] }
    private IActionResult NotFoundError(params string[] mensajes) =>
        NotFound(new { errorType = "Not Found", errors = mensajes });

    // Para múltiples mensajes de validación:
    private IActionResult BadRequestValidation(IEnumerable<string> mensajes) =>
        BadRequest(new { errorType = "Bad Request", errors = mensajes.ToArray() });


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? accountNumber) =>
        Ok(await _svc.GetAllAccountsAsync(accountNumber));


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var a = await _svc.GetAccountByIdAsync(id);
        if (a is null)
            return NotFoundError("Cuenta no encontrada");  // Get_Account_By_Id_No_Exists_NotFound
        return Ok(a);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            var lista = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            return BadRequestValidation(lista);
        }

        try
        {
            var cuenta = new Account
            {
                AccountType = dto.AccountType,
                AccountNumber = dto.AccountNumber,
                OwnerName = dto.OwnerName,
                BalanceAmount = dto.BalanceAmount
            };
            var creada = await _svc.CreateAccountAsync(cuenta);
            return Ok(creada);
        }
        catch (BusinessException ex)
        {
            // Create_Account_Exists_BadRequest
            return BadRequestError(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AccountDto dto)
    {
        if (id != dto.Id)
            return BadRequestError("El ID no coincide");    // Update_Account_Diff_Id_BadRequest

        try
        {
            var acct = new Account
            {
                Id = dto.Id,
                AccountType = dto.AccountType,
                AccountNumber = dto.AccountNumber,
                OwnerName = dto.OwnerName,
                BalanceAmount = dto.BalanceAmount
            };
            var updated = await _svc.UpdateAccountAsync(id, acct);
            return Ok(updated);                              // Update_Account_Success
        }
        catch (NotFoundException ex)
        {
            return NotFoundError(ex.Message);                // Update_Account_Id_NotFound
        }
        catch (BusinessException ex)
        {
            return BadRequestError(ex.Message);              // Update_Account_AccountNumber_NoExists_BadRequest
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _svc.DeleteAccountAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFoundError(ex.Message);
        }
    }

    [HttpPut("{id:int}/Deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto dto)
    {
        if (id != dto.Id)
            return BadRequestError("ID mismatch");         // Deposit_Account_Diff_Id_BadRequest
        try
        {
            await _svc.DepositAsync(dto.Id, dto.ValueAmount);
            return NoContent();                            // Deposit_Success
        }
        catch (NotFoundException ex)
        {
            return NotFoundError(ex.Message);              // Deposit_Account_Id_NotFound
        }
        catch (BusinessException ex)
        {
            return BadRequestError(ex.Message);            // Deposit_Account_AccountNumber_NoExists_BadRequest
        }
    }

    [HttpPut("{id:int}/Withdrawal")]
    public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto dto)
    {
        if (id != dto.Id)
            return BadRequestError("ID mismatch");         // Withdrawal_Account_Diff_Id_BadRequest
        try
        {
            await _svc.WithdrawAsync(dto.Id, dto.ValueAmount);
            return NoContent();                            // Withdrawal_Success
        }
        catch (NotFoundException ex)
        {
            return NotFoundError(ex.Message);              // Withdrawal_Account_Id_NotFound
        }
        catch (BusinessException ex)
        {
            return BadRequestError(ex.Message);            // Withdrawal_Saving_Account_Insufficient_Funds_BadRequest
        }
    }
}
