using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;

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

 
        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccountsByAccountNumberAsync([FromQuery] string? accountNumber = null)
        {
            List<Account>? accounts;

            if (string.IsNullOrEmpty(accountNumber))
            {
                accounts = await _accountService.GetAllAccounts() as List<Account>;
            }
            else
            {
                accounts = await _accountService.GetAccountsByAccountNumberAsync(accountNumber) as List<Account>;
            }

            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountById(id);

            if (account == null)
            {
                return NotFound();
            }

            var accountDto = _mapper.Map<Account, AccountDto>(account);

            return Ok(accountDto);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountDto accountDto)
        {
            var account = await _accountService.CreateAccount(_mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }


        // DELETE: api/Accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccount(id);
            return NoContent();
        }
        // PUT: api/Accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.UpdateAccount(id, _mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }
        // PUT api/Accounts/{id}/Deposit
        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            await _accountService.Deposit(id, transaction);
            return NoContent();
        }

        // PUT api/Accounts/{id}/Withdrawal
        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            await _accountService.Withdrawal(id, transaction);
            return NoContent();
        }       

    }
}