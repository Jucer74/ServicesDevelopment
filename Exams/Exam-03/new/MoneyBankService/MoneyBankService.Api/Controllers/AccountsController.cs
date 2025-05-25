using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dtos;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using System.Linq;

namespace MoneyBankService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _svc;
        public AccountsController(IAccountService svc) => _svc = svc;

        // 400 BadRequest para cualquier error (siempre camelCase)
        private IActionResult BadRequestError(params string[] mensajes) =>
            BadRequest(new { errorType = "Bad Request", errors = mensajes });

        // 400 BadRequest para validaciones con IEnumerable
        private IActionResult BadRequestValidation(IEnumerable<string> mensajes) =>
            BadRequest(new { errorType = "Bad Request", errors = mensajes.ToArray() });

        // 404 NotFound para NO ENCONTRADO (siempre camelCase)
        private IActionResult NotFoundError(params string[] mensajes) =>
            NotFound(new { errorType = "Not Found", errors = mensajes });

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? accountNumber)
        {
            var cuentas = await _svc.GetAllAccountsAsync(accountNumber);
            return Ok(cuentas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cuenta = await _svc.GetAccountByIdAsync(id);
            if (cuenta is null)
                return NotFoundError("Cuenta no encontrada");
            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateDto dto)
        {
            // 1) Validación manual de DataAnnotations
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequestValidation(errores);
            }

            // 2) Lógica de negocio
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
                return BadRequestError(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountDto dto)
        {
            // 1) Chequeo de ID
            if (id != dto.Id)
                return BadRequestError("El ID no coincide");

            // 2) Validación manual de DataAnnotations
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequestValidation(errores);
            }

            try
            {
                var cuenta = new Account
                {
                    Id = dto.Id,
                    AccountType = dto.AccountType,
                    AccountNumber = dto.AccountNumber,
                    OwnerName = dto.OwnerName,
                    BalanceAmount = dto.BalanceAmount
                };
                var actualizada = await _svc.UpdateAccountAsync(id, cuenta);
                return Ok(actualizada);
            }
            catch (NotFoundException ex)
            {
                return NotFoundError(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequestError(ex.Message);
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
                return BadRequestError("ID mismatch");

            try
            {
                await _svc.DepositAsync(dto.Id, dto.ValueAmount);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFoundError(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequestError(ex.Message);
            }
        }

        [HttpPut("{id:int}/Withdrawal")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto dto)
        {
            if (id != dto.Id)
                return BadRequestError("ID mismatch");

            try
            {
                await _svc.WithdrawAsync(dto.Id, dto.ValueAmount);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFoundError(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequestError(ex.Message);
            }
        }
    }
}
