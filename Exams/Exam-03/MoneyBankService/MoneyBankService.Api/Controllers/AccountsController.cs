using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private const int MAX_OVERDRAFT = 1_000_000;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: /api/Accounts?accountNumber=123456
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] string? accountNumber)
        {
            if (!string.IsNullOrWhiteSpace(accountNumber))
            {
                var account = await _accountService.GetAccountByNumber(accountNumber);
                return Ok(_mapper.Map<List<Account>, List<AccountDto>>(account));

            }

            var accounts = await _accountService.GetAllAccounts();
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts));
        }


        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.UpdateAccount(id, _mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountDto accountDto)
        {
            var account = await _accountService.CreateAccount(_mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // DELETE: api/Accounts/5
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

        // PUT: api/Accounts/5/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] Transaction transaction)
        {

            await _accountService.Withdrawal(id, transaction);
            return NoContent();
        }
    }
}
