using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dtos;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;

namespace MoneyBankService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _svc;
        public AccountsController(IAccountService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? accountNumber)
            => Ok(await _svc.GetAllAccountsAsync(accountNumber));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await _svc.GetAccountByIdAsync(id);
            if (a is null)
                return NotFound(new { errorType = "Not Found", errors = new[] { "Cuenta no encontrada" } });
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateDto dto)
        {
            // ModelStateInvalid ya se maneja en Program.cs
            try
            {
                var acct = new Account
                {
                    AccountType = dto.AccountType,
                    AccountNumber = dto.AccountNumber,
                    OwnerName = dto.OwnerName,
                    BalanceAmount = dto.BalanceAmount
                };
                var created = await _svc.CreateAccountAsync(acct);
                // Devuelve 200 OK en vez de 201
                return Ok(created);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { errorType = "Bad Request", errors = new[] { ex.Message } });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountCreateDto dto)
        {
            if (id != dto.GetType().GetProperty("Id")?.GetValue(dto) as int?)
                return BadRequest(new { errorType = "Bad Request", errors = new[] { "El ID no coincide" } });

            try
            {
                var acct = new Account
                {
                    Id = id,
                    AccountType = dto.AccountType,
                    AccountNumber = dto.AccountNumber,
                    OwnerName = dto.OwnerName,
                    BalanceAmount = dto.BalanceAmount
                };
                await _svc.UpdateAccountAsync(id, acct);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { errorType = "Not Found", errors = new[] { ex.Message } });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { errorType = "Bad Request", errors = new[] { ex.Message } });
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
                return NotFound(new { errorType = "Not Found", errors = new[] { ex.Message } });
            }
        }

        [HttpPut("{id:int}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto dto)
        {
            if (id != dto.Id) return BadRequest(new { errorType = "Bad Request", errors = new[] { "ID mismatch" } });
            try
            {
                await _svc.DepositAsync(dto.Id, dto.ValueAmount);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { errorType = "Not Found", errors = new[] { ex.Message } });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { errorType = "Bad Request", errors = new[] { ex.Message } });
            }
        }

        [HttpPut("{id:int}/Withdrawal")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto dto)
        {
            if (id != dto.Id) return BadRequest(new { errorType = "Bad Request", errors = new[] { "ID mismatch" } });
            try
            {
                await _svc.WithdrawAsync(dto.Id, dto.ValueAmount);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { errorType = "Not Found", errors = new[] { ex.Message } });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { errorType = "Bad Request", errors = new[] { ex.Message } });
            }
        }
    }
}
