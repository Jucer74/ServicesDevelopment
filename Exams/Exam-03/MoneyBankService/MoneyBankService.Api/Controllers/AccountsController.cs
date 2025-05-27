using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountDto>>> GetAccounts([FromQuery] string accountNumber = null!)
        {
            if (accountNumber == null)
            {
                var accounts = await _accountService.GetAllAccountsAsync();
                return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts));
            }

            var account = await _accountService.GetAccountsByAccountNumberAsync(accountNumber);
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(account));
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<ActionResult<AccountDto>> Post([FromBody] AccountDto accountDto)
        {
            var newAccount = await _accountService.CreateAccountAsync(_mapper.Map<AccountDto, Account>(accountDto));

            return Ok(_mapper.Map<Account, AccountDto>(newAccount));
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AccountDto accountDto)
        {
            var newAccount = await _accountService.UpdateAccount(id, _mapper.Map<AccountDto, Account>(accountDto));

            return Ok(_mapper.Map<Account, AccountDto>(newAccount));
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _accountService.DeleteAccountAsync(id);

            return NoContent();
        }

        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            await _accountService.DepositAsync(id, _mapper.Map<TransactionDto, Transaction>(transactionDto));

            return NoContent();
        }

        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transactionDto)
        {
            await _accountService.WithdrawAsync(id, _mapper.Map<TransactionDto, Transaction>(transactionDto));

            return NoContent();
        }
    }
}
