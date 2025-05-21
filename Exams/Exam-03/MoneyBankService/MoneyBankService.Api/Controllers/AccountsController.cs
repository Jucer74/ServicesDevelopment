using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Infrastructure.Context;
using MoneyBankService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const decimal MAX_OVERDRAFT = 1000000m;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] string? accountNumber)
        {
            if (!string.IsNullOrWhiteSpace(accountNumber))
            {
                var accounts = await _context.Accounts
                    .Where(a => a.AccountNumber == accountNumber)
                    .ToListAsync();
                return Ok(accounts);
            }

            var allAccounts = await _context.Accounts.ToListAsync();
            return Ok(allAccounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountDto dto)
        {
            if (dto.BalanceAmount <= 0)
                return BadRequest(new { message = "El Balance debe ser mayor a cero" });

            var account = new Account
            {
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                OwnerName = dto.OwnerName,
                CreationDate = dto.CreationDate,
                OverdraftAmount = dto.OverdraftAmount
            };

            if (dto.AccountType == 'C')
            {
                account.BalanceAmount = dto.BalanceAmount + MAX_OVERDRAFT;
                account.OverdraftAmount = MAX_OVERDRAFT;
            }
            else
            {
                account.BalanceAmount = dto.BalanceAmount;
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cuenta creada exitosamente", dto });
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            account.AccountNumber = dto.AccountNumber;
            account.AccountType = dto.AccountType;
            account.OwnerName = dto.OwnerName;
            account.BalanceAmount = dto.BalanceAmount;
            account.OverdraftAmount = dto.OverdraftAmount;
            account.CreationDate = dto.CreationDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Accounts/{id}/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            account.BalanceAmount += dto.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
                else
                {
                    account.OverdraftAmount = 0;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Accounts/{id}/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            if (dto.ValueAmount > account.BalanceAmount)
                return BadRequest(new { message = "Fondos Insuficientes" });

            account.BalanceAmount -= dto.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}