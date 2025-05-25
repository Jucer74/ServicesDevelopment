using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Common;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private const decimal MAX_OVERDRAFT = 1_000_000M;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync(string? AccountNumber)
        {
            IEnumerable<BankAccount> accounts;
            if (!string.IsNullOrEmpty(AccountNumber))
                accounts = await _accountRepository.FindAsync(x => x.AccountNumber == AccountNumber);
            else
                accounts = await _accountRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account == null ? null : _mapper.Map<AccountDto>(account);
        }

        public async Task<ServiceResult<AccountDto>> CreateAccountAsync(AccountDto accountDto)
        {
            if (string.IsNullOrWhiteSpace(accountDto.OwnerName) ||
                string.IsNullOrWhiteSpace(accountDto.AccountNumber) ||
                accountDto.BalanceAmount <= 0)
            {
                return ServiceResult<AccountDto>.Fail("El Balance debe ser mayor a cero");
            }

            var exists = (await _accountRepository.FindAsync(x => x.AccountNumber == accountDto.AccountNumber)).Any();
            if (exists)
                return ServiceResult<AccountDto>.Fail($"La Cuenta [{accountDto.AccountNumber}] ya Existe");

            var entity = _mapper.Map<BankAccount>(accountDto);

            if (entity.AccountType == 'C')
            {
                entity.BalanceAmount += MAX_OVERDRAFT;
                entity.OverdraftAmount = 0;
            }

            var created = await _accountRepository.AddAsync(entity);
            return ServiceResult<AccountDto>.Success(_mapper.Map<AccountDto>(created));
        }

        public async Task<ServiceResult> UpdateAccountAsync(AccountDto accountDto)
        {
            var exists = (await _accountRepository.FindAsync(x => x.AccountNumber == accountDto.AccountNumber)).Any();
            if (!exists)
                return ServiceResult.Fail($"La Cuenta [{accountDto.AccountNumber}] No Existe");

            var entity = _mapper.Map<BankAccount>(accountDto);
            await _accountRepository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAccountAsync(int id)
        {
            var entity = await _accountRepository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Fail("Cuenta no encontrada");

            await _accountRepository.RemoveAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DepositAsync(int id, TransactionDto transactionDto)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                return ServiceResult.Fail("Cuenta no encontrada");

            if (account.AccountNumber != transactionDto.AccountNumber)
                return ServiceResult.Fail("El Numero de la Cuenta es Diferente al de la transaccion");

            account.BalanceAmount += transactionDto.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                else
                    account.OverdraftAmount = 0;
            }
            else
            {
                account.OverdraftAmount = 0;
            }

            await _accountRepository.UpdateAsync(account);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> WithdrawalAsync(int id, TransactionDto transactionDto)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                return ServiceResult.Fail("Cuenta no encontrada");

            if (account.BalanceAmount < transactionDto.ValueAmount)
                return ServiceResult.Fail("Fondos Insuficientes");

            account.BalanceAmount -= transactionDto.ValueAmount;

            if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            else
                account.OverdraftAmount = 0;

            await _accountRepository.UpdateAsync(account);
            return ServiceResult.Success();
        }
    }
}