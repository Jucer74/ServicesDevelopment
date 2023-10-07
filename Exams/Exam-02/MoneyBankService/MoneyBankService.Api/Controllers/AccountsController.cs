using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            var accountDtos = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountDto>>(accounts);
            return Ok(accountDtos);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);
            var createdAccount = await _accountService.CreateAccount(account);
            var createdAccountDto = _mapper.Map<Account, AccountDto>(createdAccount);
            return Ok(createdAccountDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountDto = _mapper.Map<Account, AccountDto>(account);
            return Ok(accountDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto accountDto)
        {
            var account = _mapper.Map<AccountDto, Account>(accountDto);
            var updatedAccount = await _accountService.UpdateAccount(id, account);
            if (updatedAccount == null)
            {
                return NotFound();
            }
            var updatedAccountDto = _mapper.Map<Account, AccountDto>(updatedAccount);
            return Ok(updatedAccountDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            await _accountService.DeleteAccount(id);
            return Ok();
        }

        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            var account = await _accountService.DepositToAccount(id, transaction);
            if (account == null)
            {
                return NotFound();
            }
            var accountDto = _mapper.Map<Account, AccountDto>(account);
            return Ok(accountDto);
        }

        [HttpPut("{id}/Withdrawal")]
        public async Task<IActionResult> Withdrawal(int id, [FromBody] TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            var account = await _accountService.WithdrawalFromAccount(id, transaction);
            if (account == null)
            {
                return NotFound();
            }
            var accountDto = _mapper.Map<Account, AccountDto>(account);
            return Ok(accountDto);
        }
    }
}
