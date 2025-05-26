using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Dto;
using MoneyBankService.Api.Middleware;

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

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] string? accountNumber)
        {
            var result = await _accountService.GetAccountsAsync(accountNumber);
            return Ok(result);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            if (result == null)
                return NotFound(new
                {
                    ErrorType = "Not Found",
                    Errors = new List<string> { "Cuenta no encontrada" }
                });
            return Ok(result);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Para validaciones de modelo, usa camelCase (como espera la colección)
                return BadRequest(new ErrorDetails
                {
                    ErrorType = "Bad Request",
                    Errors = errors
                });
            }

            var result = await _accountService.CreateAccountAsync(accountDto);
            if (!result.Success)
            {
                // Para errores de negocio, usa PascalCase (como espera la colección)
                return BadRequest(new
                {
                    ErrorType = "Bad Request",
                    Errors = new List<string> { result.ErrorMessage ?? "Error al crear la cuenta" }
                });
            }
            return Ok(result.Data);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto accountDto)
        {
            if (id != accountDto.Id)
                return BadRequest(new
                {
                    ErrorType = "Bad Request",
                    Errors = new List<string> { "El id de la URL no coincide con el id del body" }
                });

            var result = await _accountService.UpdateAccountAsync(accountDto);
            if (!result.Success)
            {
                // Si la cuenta no existe, responde 404 con PascalCase
                if (result.ErrorMessage != null && result.ErrorMessage.Contains("No Existe"))
                {
                    return NotFound(new
                    {
                        ErrorType = "Not Found",
                        Errors = new List<string> { result.ErrorMessage }
                    });
                }
                // Otros errores de negocio
                return BadRequest(new
                {
                    ErrorType = "Bad Request",
                    Errors = new List<string> { result.ErrorMessage ?? "Error al actualizar la cuenta" }
                });
            }
            // Para éxito en update, responde 200 con el objeto actualizado si la prueba lo espera, si no, NoContent.
            // La colección espera 200 OK y el objeto actualizado en algunos tests, así que:
            return Ok(accountDto);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (!result.Success)
                return NotFound(new
                {
                    ErrorType = "Not Found",
                    Errors = new List<string> { result.ErrorMessage ?? "Cuenta no encontrada" }
                });
            return NoContent();
        }

        // PUT: api/Accounts/5/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            var result = await _accountService.DepositAsync(id, transactionDto);
            if (!result.Success)
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Contains("Cuenta no encontrada"))
                {
                    return NotFound(new
                    {
                        ErrorType = "Not Found",
                        Errors = new List<string> { result.ErrorMessage }
                    });
                }
                return BadRequest(new
                {
                    ErrorType = "Bad Request",
                    Errors = new List<string> { result.ErrorMessage ?? "Error en el depósito" }
                });
            }
            return NoContent();
        }

        // PUT: api/Accounts/5/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transactionDto)
        {
            var result = await _accountService.WithdrawalAsync(id, transactionDto);
            if (!result.Success)
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Contains("Cuenta no encontrada"))
                {
                    return NotFound(new
                    {
                        ErrorType = "Not Found",
                        Errors = new List<string> { result.ErrorMessage }
                    });
                }
                return BadRequest(new
                {
                    ErrorType = "Bad Request",
                    Errors = new List<string> { result.ErrorMessage ?? "Error en el retiro" }
                });
            }
            return NoContent();
        }
    }
}
