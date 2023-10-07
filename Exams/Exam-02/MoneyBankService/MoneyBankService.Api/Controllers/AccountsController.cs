using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using Org.BouncyCastle.Utilities.IO;


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
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccounts() as List<Account>;
            return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountDto accountDto)
        {
            var account = await _accountService.CreateAccount(_mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.UpdateAccount(id, _mapper.Map<AccountDto, Account>(accountDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(int id)
        {
            await _accountService.DeleteAccount(id);
            return Ok();
        }

        [HttpPut("{id}/Deposit")]
        public async Task<IActionResult> Deposit(int id, TransactionDto transactionDto)
        {
            var account = await _accountService.DepositToAccount(id, _mapper.Map<TransactionDto, Transaction>(transactionDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }

        [HttpPut("{id}/WithDrawal")]
        public async Task<IActionResult> WithDrawal(int id, TransactionDto transactionDto)
        {
            var account = await _accountService.WithDrawalToAccount(id, _mapper.Map<TransactionDto, Transaction>(transactionDto));
            return Ok(_mapper.Map<Account, AccountDto>(account));
        }
    }
}