using AutoMapper;
using MoneyBankService02.Domain.Interfaces.Repositories;
using MoneyBankService02.Application.Common;
using MoneyBankService02.Application.Dto;
using MoneyBankService02.Application.Interfaces;
using MoneyBankService02.Domain.Entities;

namespace MoneyBankService02.Application.Services;

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

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync(string? accountNumber)
    {
        var accounts = string.IsNullOrEmpty(accountNumber)
            ? await _accountRepository.GetAllAsync()
            : await _accountRepository.FindAsync(x => x.AccountNumber == accountNumber);

        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<AccountDto?> GetAccountByIdAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        return account == null ? null : _mapper.Map<AccountDto>(account);
    }

    public async Task<ServiceResult<AccountDto>> CreateAccountAsync(AccountDto accountDto)
    {
        if (string.IsNullOrWhiteSpace(accountDto.OwnerName) || accountDto.BalanceAmount <= 0)
            return ServiceResult<AccountDto>.Fail("El balance debe ser mayor a cero");

        var exists = (await _accountRepository.FindAsync(x => x.AccountNumber == accountDto.AccountNumber)).Any();
        if (exists)
            return ServiceResult<AccountDto>.Fail($"La cuenta [{accountDto.AccountNumber}] ya existe");

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
        var entity = await _accountRepository.GetByIdAsync(accountDto.Id);
        if (entity == null)
            return ServiceResult.Fail($"La cuenta [{accountDto.AccountNumber}] no existe");

        await _accountRepository.UpdateAsync(_mapper.Map<BankAccount>(accountDto));
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
            return ServiceResult.Fail("El número de cuenta en la transacción no coincide");

        account.BalanceAmount += transactionDto.ValueAmount;

        if (account.AccountType == 'C')
        {
            if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            else
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
            return ServiceResult.Fail("Fondos insuficientes");

        account.BalanceAmount -= transactionDto.ValueAmount;

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        else
            account.OverdraftAmount = 0;

        await _accountRepository.UpdateAsync(account);
        return ServiceResult.Success();
    }
}
