using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Dto;
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

        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts() as List<Account>;
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
        }

        // GET api/<AccountsController>/ByAccountNumber?accountNumber=123456789
        [HttpGet("ByAccountNumber")]
        public async Task<IActionResult> GetAccountByAccountNumber([FromQuery] string accountNumber)
        {
            var accounts = await _accountService.FindAccountsByAccountNumber(accountNumber) as List<Account>;
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);

            return Ok(_mapper.Map<Account, AccountDto>(await _accountService.CreateAccount(account)));
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);
            return Ok(_mapper.Map<Account, AccountDto>(await _accountService.UpdateAccount(id, account)));
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteAccount(id);
            return Ok();
        }

        //PUT api/<AccountsController>/5/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            await _accountService.Deposit(id, transactionDto);
            return Ok();
        }

        //PUT api/<AccountsController>/5/Withdraw
        [HttpPut("{id}/Withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto transactionDto)
        {
            await _accountService.Withdraw(id, transactionDto);
            return Ok();
        }

    }
}
