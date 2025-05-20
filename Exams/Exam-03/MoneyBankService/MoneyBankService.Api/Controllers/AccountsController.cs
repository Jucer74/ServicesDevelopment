using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.DTO;
using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Services.Interfaces;

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

        

        // GET: api/Accounts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            return Ok(account);
        }


        // GET: api/Accounts?accountNumber=1234567890
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                var accounts = await _accountService.GetAllAsync();
                return Ok(accounts); 
            }

            var account = await _accountService.GetByAccountNumberAsync(accountNumber);

           
            if (account == null)
            {
                return Ok(Array.Empty<AccountCreateDto>()); 
            }

            return Ok(new[] { account });
        }



        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto dto)
        {
            
            var createdAccount = await _accountService.CreateAsync(dto);

            return Ok(createdAccount); 
        }


        // PUT: api/Accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountResultDto dto)
        {
            
            if (id != dto.Id)
                throw new BadRequestException("El ID de la URL no coincide con el ID del cuerpo.");
            

            var account = await _accountService.GetByIdAsync(id);
            if (dto.AccountNumber != account!.AccountNumber)
            {
                throw new BadRequestException("Numero de cuenta no coincide");
            }
                if (account == null)
                throw new NotFoundException($"Cuenta con ID [{id}] no encontrada.");

            await _accountService.UpdateAsync(id, dto);

            var updated = await _accountService.GetByIdAsync(id);
            return Ok(updated);
        }


        // DELETE: api/Accounts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/Accounts/{id}/deposit
        [HttpPut("{id:int}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionCreateDto dto)
        {

            if (id != dto.Id)
                throw new BadRequestException("El ID de la URL no coincide con el ID del cuerpo.");
            await _accountService.DepositAsync(id, dto);
            return NoContent();
        }

        // PUT: api/Accounts/{id}/withdrawal
        [HttpPut("{id:int}/withdrawal")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionCreateDto dto)
        {
            if (id != dto.Id)
                throw new BadRequestException("El ID de la URL no coincide con el ID del cuerpo.");
            await _accountService.WithdrawAsync(id, dto);
            return NoContent();
        }
    }
}
