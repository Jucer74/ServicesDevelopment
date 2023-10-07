using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankAPI.Context;
using MoneyBankAPI.Models;

namespace MoneyBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private const int MAX_OVERDRAFT = 1_000_000;

        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts([FromQuery]string accountNumber=null!)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrEmpty(accountNumber))
            {
                return await _context.Accounts.Where(x => x.AccountNumber == accountNumber).ToListAsync();
            }

            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            if (!ExistAccountNumber(account.AccountNumber))
            {
                return BadRequest($"La Cuenta [{account.AccountNumber}] No Existe");
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'AppDbContext.Accounts'  is null.");
            }

            if (ExistAccountNumber(account.AccountNumber))
            {
                return BadRequest($"La Cuenta [{account.AccountNumber}] ya Existe");
            }

            if(account.BalanceAmount <= 0)
            {
                return BadRequest("El Balance debe ser mayor a cero");
            }

            if(account.AccountType == 'C')
            {
                account.BalanceAmount += MAX_OVERDRAFT;
            }
            
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Accounts/5/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!AccountExists(id))
            {
                return NotFound();
            }

            var account = FindAccount(id);

            if (account.AccountNumber != transaction.AccountNumber)
            {
                return BadRequest("El Numero de la Cuenta es Diferente al de la transaccion");
            }

            UpdateDepositValue(account, transaction);

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Accounts/5/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!AccountExists(id))
            {
                return NotFound();
            }

            var account = FindAccount(id);

            if (!AccountHasSufficientFunds(account, transaction))
            {
                return BadRequest("Fondos Insuficientes");
            }

            UpdateWithdrawalValue(account, transaction);

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #region Private-Methods

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private Account FindAccount(int id)
        {
            return _context.Accounts?.FirstOrDefault(e => e.Id == id)!;
        }

        private bool ExistAccountNumber(string accountNumber)
        {
            return (_context.Accounts?.Any(e => e.AccountNumber == accountNumber)).GetValueOrDefault();
        }

        private bool AccountHasSufficientFunds(Account account, Transaction transaction)
        {
            return account.BalanceAmount >= transaction.ValueAmount;
        }

        private void UpdateDepositValue(Account account, Transaction transaction)
        {
            account.BalanceAmount += transaction.ValueAmount;

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
        }

        private void UpdateWithdrawalValue(Account account, Transaction transaction)
        {
            account.BalanceAmount -= transaction.ValueAmount;

            if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
        }
    }

    #endregion Private-Methods
}
