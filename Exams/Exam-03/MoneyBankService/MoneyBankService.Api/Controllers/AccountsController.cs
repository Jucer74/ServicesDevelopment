using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts([FromQuery] string? accountNumber)
        {
            if (!string.IsNullOrEmpty(accountNumber))
            {
                var account = await _accountService.GetAccountByAccountNumber(accountNumber);
                if (account == null)
                {
                    return Ok(new List<AccountDto>());
                }

                var resultList = new List<Account> { account };
                return Ok(_mapper.Map<List<Account>, List<AccountDto>>(resultList));
            }

            var accounts = await _accountService.GetAllAccounts();
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts));
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);
            var created = await _accountService.CreateAccount(account);
            return Ok(_mapper.Map<Account, AccountDto>(created));
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);
            var updated = await _accountService.UpdateAccount(id, account);
            return Ok(_mapper.Map<Account, AccountDto>(updated));
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccount(id);
            return NoContent();
        }

        // PUT: api/Accounts/Deposit
        [HttpPut("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            await _accountService.Deposit(id, transaction);
            return NoContent();
        }

        // PUT: api/Accounts/Withdrawal
        [HttpPut("{id}/withdrawal")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            await _accountService.Withdraw(id, transaction);
            return NoContent();
        }
    }
}
