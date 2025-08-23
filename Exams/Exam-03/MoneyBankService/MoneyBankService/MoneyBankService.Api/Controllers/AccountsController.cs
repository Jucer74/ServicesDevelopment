using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Dto;
using MoneyBankService.Api.Middleware;
using MoneyBankService.Application.Common;

namespace MoneyBankService.Api.Controllers
{
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
        public async Task<IActionResult> GetAll([FromQuery] string? AccountNumber)
        {
            var accounts = await _accountService.GetAccountsAsync(AccountNumber);
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

            if (!result.IsSuccess)
            {
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "ValidationError",
                    Errors = new List<string> { result.Message ?? "Datos de cuenta inválidos" }
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountDto accountDto)
        {
            if (id != accountDto.Id)
            {
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "IdMismatch",
                    Errors = new List<string> { "El ID de la ruta no coincide con el ID de la cuenta" }
                });
            }

            var result = await _accountService.UpdateAccountAsync(accountDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "UpdateError",
                    Errors = new List<string> { result.Message ?? "Error al actualizar la cuenta" }
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new ErrorDetails
                {
                    ErrorType = "NotFound",
                    Errors = new List<string> { result.Message ?? "Cuenta no encontrada" }
                });
            }

            return NoContent();
        }

        [HttpPut("{id}/deposit")]
        public async Task<IActionResult> DepositFunds(int id, [FromBody] TransactionDto transaction)
        {
            var result = await _accountService.DepositAsync(id, transaction);

            if (!result.IsSuccess)
            {
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "TransactionError",
                    Errors = new List<string> { result.Message ?? "Error en la operación de depósito" }
                });
            }

            return NoContent();
        }

        [HttpPut("{id}/withdraw")]
        public async Task<IActionResult> WithdrawFunds(int id, [FromBody] TransactionDto transaction)
        {
            var result = await _accountService.WithdrawalAsync(id, transaction);

            if (!result.IsSuccess)
            {
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "TransactionError",
                    Errors = new List<string> { result.Message ?? "Error en la operación de retiro" }
                });
            }

            return NoContent();
        }
    }
}