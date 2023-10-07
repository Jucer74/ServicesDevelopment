using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // Vars
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        // Constructor
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            // Get all accounts
            var accounts = await _accountService.GetAllAccountsAsync();
            // Map accounts to accountDtos
            var accountDtos = _mapper.Map<IEnumerable<Accounts>, IEnumerable<AccountDto>>(accounts);
            return Ok(accountDtos);
        }

        // GET: api/<AccountsController>/GetAccountByNumber/123456789
        [HttpGet("accountNumber/{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(string accountNumber)
        {
            // Get account by account number
            var account = await _accountService.GetAccountAsync(accountNumber);
            // Map account to accountDto
            var accountDto = _mapper.Map<Accounts, AccountDto>(account!);
            return Ok(accountDto);
        }

        // GET api/<AccountsController>/5
        [HttpGet("accountId/{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            // Get account by id
            var account = await _accountService.GetAccountByIdAsync(id);
            // Map account to accountDto
            var accountDto = _mapper.Map<Accounts, AccountDto>(account);
            return Ok(accountDto);
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Accounts account)
        {
            // Post account
            var accountAdded = await _accountService.AddAccountAsync(account);
            // Map account to accountDto
            var accountAddedDto = _mapper.Map<Accounts, AccountDto>(accountAdded);
            return Ok(accountAddedDto);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Accounts account)
        {
            // Put account
            var accountUpdated = await _accountService.UpdateAccountAsync(id, account);
            // Map account to accountDto
            var accountUpdatedDto = _mapper.Map<Accounts, AccountDto>(accountUpdated);
            return Ok(accountUpdatedDto);
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            // Delete account
            var accountDeleted = await _accountService.DeleteAccountAsync(id);
            // Map account to accountDto
            _mapper.Map<Accounts, AccountDto>(accountDeleted);
            return Ok();
        }

        // PUT api/<AccountsController>/Deposit/123456789/100
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] Transactions transaction)
        {
            // Put Deposit
            var updatedAccount = await _accountService.DepositAsync(id, transaction);
            // Map account to accountDto
            _mapper.Map<Accounts, AccountDto>(updatedAccount);
            return Ok();
        }


        // PUT api/<AccountsController>/Withdraw/123456789/100
        [HttpPut("{id}/Withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] Transactions transaction)
        {
            // Put Withdraw
            var updatedAccount = await _accountService.WithdrawAsync(id, transaction);
            // Map account to accountDto
            _mapper.Map<Accounts, AccountDto>(updatedAccount);
            return Ok();
        }
    }
}