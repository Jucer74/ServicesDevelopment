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
        Console.WriteLine($"Buscando cuentas con AccountNumber: {accountNumber}");
        var accounts = string.IsNullOrEmpty(accountNumber)
            ? await _accountRepository.GetAllAsync()
            : await _accountRepository.FindAsync(x => x.AccountNumber == accountNumber);

        Console.WriteLine($"Cuentas encontradas: {accounts.Count()}");
        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<AccountDto?> GetAccountByIdAsync(int id)
    {
        Console.WriteLine($"Buscando cuenta con ID: {id}");
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null) Console.WriteLine("⚠ Cuenta no encontrada");
        return account == null ? null : _mapper.Map<AccountDto>(account);
    }

    public async Task<ServiceResult<AccountDto>> CreateAccountAsync(AccountDto accountDto)
    {
        Console.WriteLine($"Creando cuenta con AccountNumber: {accountDto.AccountNumber}");

        if (string.IsNullOrWhiteSpace(accountDto.OwnerName) || accountDto.BalanceAmount <= 0)
        {
            Console.WriteLine(" Validación fallida: Nombre vacío o balance menor o igual a 0");
            return ServiceResult<AccountDto>.Fail("El balance debe ser mayor a cero");
        }

        var exists = (await _accountRepository.FindAsync(x => x.AccountNumber == accountDto.AccountNumber)).Any();
        if (exists)
        {
            Console.WriteLine($"La cuenta [{accountDto.AccountNumber}] ya existe");
            return ServiceResult<AccountDto>.Fail($"La cuenta [{accountDto.AccountNumber}] ya existe");
        }

        var entity = _mapper.Map<BankAccount>(accountDto);
        if (entity.AccountType == 'C')
        {
            Console.WriteLine("Aplicando sobregiro inicial para cuenta tipo 'C'");
            entity.BalanceAmount += MAX_OVERDRAFT;
            entity.OverdraftAmount = 0;
        }

        var created = await _accountRepository.AddAsync(entity);
        Console.WriteLine($"Cuenta creada exitosamente con ID: {created.Id}");
        return ServiceResult<AccountDto>.Success(_mapper.Map<AccountDto>(created));
    }

    public async Task<ServiceResult> UpdateAccountAsync(AccountDto accountDto)
    {
        Console.WriteLine($"Actualizando cuenta con ID: {accountDto.Id}");
        var entity = await _accountRepository.GetByIdAsync(accountDto.Id);
        if (entity == null)
        {
            Console.WriteLine("Cuenta no encontrada");
            return ServiceResult.Fail($"La cuenta [{accountDto.AccountNumber}] no existe");
        }

        await _accountRepository.UpdateAsync(_mapper.Map<BankAccount>(accountDto));
        Console.WriteLine("Cuenta actualizada exitosamente");
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteAccountAsync(int id)
    {
        Console.WriteLine($"Eliminando cuenta con ID: {id}");
        var entity = await _accountRepository.GetByIdAsync(id);
        if (entity == null)
        {
            Console.WriteLine("Cuenta no encontrada");
            return ServiceResult.Fail("Cuenta no encontrada");
        }

        await _accountRepository.RemoveAsync(entity);
        Console.WriteLine("Cuenta eliminada correctamente");
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DepositAsync(int id, TransactionDto transactionDto)
    {
        Console.WriteLine($"Procesando depósito en cuenta ID: {id} - Monto: {transactionDto.ValueAmount}");
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            Console.WriteLine("Cuenta no encontrada");
            return ServiceResult.Fail("Cuenta no encontrada");
        }

        if (account.AccountNumber != transactionDto.AccountNumber)
        {
            Console.WriteLine("Error: El número de cuenta en la transacción no coincide");
            return ServiceResult.Fail("El número de cuenta en la transacción no coincide");
        }

        account.BalanceAmount += transactionDto.ValueAmount;
        Console.WriteLine($"Nuevo saldo: {account.BalanceAmount}");

        if (account.AccountType == 'C')
        {
            if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            else
                account.OverdraftAmount = 0;
        }

        await _accountRepository.UpdateAsync(account);
        Console.WriteLine("Depósito realizado correctamente");
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> WithdrawalAsync(int id, TransactionDto transactionDto)
    {
        Console.WriteLine($"Procesando retiro en cuenta ID: {id} - Monto: {transactionDto.ValueAmount}");
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            Console.WriteLine("Cuenta no encontrada");
            return ServiceResult.Fail("Cuenta no encontrada");
        }

        if (account.BalanceAmount < transactionDto.ValueAmount)
        {
            Console.WriteLine("Fondos insuficientes para realizar el retiro");
            return ServiceResult.Fail("Fondos insuficientes");
        }

        account.BalanceAmount -= transactionDto.ValueAmount;
        Console.WriteLine($"Nuevo saldo: {account.BalanceAmount}");

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        else
            account.OverdraftAmount = 0;

        await _accountRepository.UpdateAsync(account);
        Console.WriteLine("Retiro realizado correctamente");
        return ServiceResult.Success();
    }
}
