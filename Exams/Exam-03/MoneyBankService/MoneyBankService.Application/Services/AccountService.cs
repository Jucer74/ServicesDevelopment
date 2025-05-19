using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> CreateAccountAsync(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            var createdAccount = await _accountRepository.CreateAsync(account);
            return _mapper.Map<AccountDto>(createdAccount);
        }

        public async Task<bool> UpdateAccountAsync(int id, AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            return await _accountRepository.UpdateAsync(id, account);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _accountRepository.DeleteAsync(id);
        }
    }
}