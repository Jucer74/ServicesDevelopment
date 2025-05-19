using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;

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
        public async Task<IActionResult> GetAccounts([FromQuery]string accountNumber =null!)
        {
            var accounts = await _accountService.GetAccounts(accountNumber);
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts));
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
            var account = await _accountService.CreateAccount(_mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.UpdateAccount(id, _mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteAccount(id);
            return NoContent();
        }

        // PUT: api/Accounts/5/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] Transaction transaction)
        {

            await _accountService.Deposit(id, transaction);
            return NoContent();
        }

        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] Transaction transaction)
        {

            await _accountService.Withdraw(id, transaction);
            return NoContent();
        }
    }
}
