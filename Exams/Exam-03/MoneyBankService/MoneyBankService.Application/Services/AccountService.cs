using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private const decimal MAX_OVERDRAFT = 1_000_000M;

    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync(string? accountNumber)
    {
        IEnumerable<Account> accounts = string.IsNullOrEmpty(accountNumber)
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
        var validationError = ValidateAccountForCreation(accountDto);
        if (validationError != null)
            return ServiceResult<AccountDto>.Fail(validationError);

        if (await AccountExistsAsync(accountDto.AccountNumber))
            return ServiceResult<AccountDto>.Fail($"La Cuenta [{accountDto.AccountNumber}] ya Existe");

        var entity = _mapper.Map<Account>(accountDto);

        if (entity.AccountType == 'C')
        {
            entity.BalanceAmount += MAX_OVERDRAFT;
            entity.OverdraftAmount = 0;
        }

        var created = await _accountRepository.AddAsync(entity);
        return ServiceResult<AccountDto>.Ok(_mapper.Map<AccountDto>(created));
    }

    public async Task<ServiceResult> UpdateAccountAsync(AccountDto accountDto)
    {
        if (!await AccountExistsAsync(accountDto.AccountNumber))
            return ServiceResult.Fail($"La Cuenta [{accountDto.AccountNumber}] No Existe");

        var entity = _mapper.Map<Account>(accountDto);
        await _accountRepository.UpdateAsync(entity);
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAccountAsync(int id)
    {
        var entity = await _accountRepository.GetByIdAsync(id);
        if (entity == null)
            return ServiceResult.Fail("Cuenta no encontrada");

        await _accountRepository.RemoveAsync(entity);
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DepositAsync(int id, TransactionDto transactionDto)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
            return ServiceResult.Fail("Cuenta no encontrada");

        if (!IsAccountNumberMatch(account, transactionDto))
            return ServiceResult.Fail("El Numero de la Cuenta es Diferente al de la transaccion");

        account.BalanceAmount += transactionDto.ValueAmount;

        if (account.AccountType == 'C')
            account.OverdraftAmount = (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                ? MAX_OVERDRAFT - account.BalanceAmount
                : 0;
        else
            account.OverdraftAmount = 0;

        await _accountRepository.UpdateAsync(account);
        return ServiceResult.Ok();
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
        return ServiceResult.Ok();
    }

    private async Task<bool> AccountExistsAsync(string accountNumber)
    {
        return (await _accountRepository.FindAsync(x => x.AccountNumber == accountNumber)).Any();
    }

    private static bool IsAccountNumberMatch(Account account, TransactionDto transactionDto)
    {
        return account.AccountNumber == transactionDto.AccountNumber;
    }

    private static string? ValidateAccountForCreation(AccountDto accountDto)
    {
        if (string.IsNullOrWhiteSpace(accountDto.OwnerName) ||
            string.IsNullOrWhiteSpace(accountDto.AccountNumber) ||
            accountDto.BalanceAmount <= 0)
        {
            return "El Balance debe ser mayor a cero";
        }
        return null;
    }
}