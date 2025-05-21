using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Application.Dto;
using AutoMapper;
using MoneyBankService.Application.Interfaces.Services;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        
        private readonly IMapper _mapper;

        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] string? accountNumber = null)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                var accounts = await _accountService.GetAllAsync();
                var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
                return Ok(accountsDto);
            }

            var account = await _accountService.GetAccountByAccountNumber(accountNumber);

            var accountDto = _mapper.Map<List<AccountDto>>(account);
            return Ok(accountDto);
        }


        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return Ok(accountDto);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.UpdateAsync(id, _mapper.Map<Account>(accountDto));
            return Ok(_mapper.Map<AccountDto>(account));
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] AccountDto accountDto)
        {
           var account = await _accountService.CreateAsync(_mapper.Map<Account>(accountDto));
            return Ok(_mapper.Map<AccountDto>(account));
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/Accounts/5/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            await _accountService.UpdateValue(id, transaction, 'D');

            return NoContent();
        }

        // PUT: api/Accounts/5/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            await _accountService.UpdateValue(id, transaction, 'W');

            return NoContent();
        }
    }
}