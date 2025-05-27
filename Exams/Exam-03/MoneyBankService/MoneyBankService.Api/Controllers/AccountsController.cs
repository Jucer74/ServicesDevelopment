using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyBankService.Api.Middleware; // <--- ASEGÚRATE DE TENER ESTE USING
using Microsoft.AspNetCore.Http;      // <--- Y ESTE USING PARA StatusCodes

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts([FromQuery] string? accountNumber = null)
        {
            if (!string.IsNullOrEmpty(accountNumber))
            {
                var accountsByNumber = await _accountService.GetAccountsByAccountNumberAsync(accountNumber);
                return Ok(accountsByNumber);
            }

            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                // --- MODIFICACIÓN AQUÍ ---
                var errorResponse = new ErrorDetails
                {
                    ErrorType = "Not Found", // Esto será PascalCase por la definición en ErrorDetails
                    Errors = new List<string> { $"Account with Id={id} Not Found" }
                };
                // Devuelve un ContentResult para usar la serialización de Newtonsoft.Json de ErrorDetails.ToString()
                return new ContentResult
                {
                    Content = errorResponse.ToString(),
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status404NotFound
                };
                // --- FIN DE LA MODIFICACIÓN ---
            }
            return Ok(account); // Esto usará System.Text.Json (configurado para camelCase en Program.cs)
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> PostAccount(AccountDto accountDto)
        {
            var createdAccount = await _accountService.CreateAccountAsync(accountDto);
            return Ok(createdAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountDto accountDto)
        {
            var updatedAccount = await _accountService.UpdateAccountAsync(id, accountDto);
            return Ok(updatedAccount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var success = await _accountService.DeleteAccountAsync(id);
            if (success)
            {
                return NoContent();
            }
            return BadRequest("Delete operation failed for an unknown reason.");
        }

        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, TransactionDto transactionDto)
        {
            var success = await _accountService.DepositAsync(id, transactionDto);
            if (success)
            {
                return NoContent();
            }
            return BadRequest("Deposit operation failed for an unknown reason.");
        }

        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, TransactionDto transactionDto)
        {
            var success = await _accountService.WithdrawAsync(id, transactionDto);
            if (success)
            {
                return NoContent();
            }
            return BadRequest("Withdrawal operation failed for an unknown reason.");
        }
    }
}