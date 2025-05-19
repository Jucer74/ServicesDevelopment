using AutoMapper;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Api.Dto;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private const decimal MAX_OVERDRAFT = 1_000_000;
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountDto>> GetAllAsync()
    {
        var accounts = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<AccountDto> GetByIdAsync(int id)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null)
            throw new NotFoundException($"La cuenta con id [{id}] no existe");
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<IEnumerable<AccountDto>> FindByAccountNumberAsync(string accountNumber)
    {
        var accounts = await _repository.FindAsync(x => x.AccountNumber == accountNumber);
        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<AccountDto> CreateAsync(AccountDto dto)
    {
        var exists = await _repository.FindAsync(x => x.AccountNumber == dto.AccountNumber);
        if (exists.Any())
            throw new BadRequestException($"La Cuenta [{dto.AccountNumber}] ya Existe");

        if (dto.BalanceAmount <= 0)
            throw new BadRequestException("El Balance debe ser mayor a cero");

        var account = _mapper.Map<Account>(dto);

        if (account.AccountType == 'C') // Corriente
        {
            account.BalanceAmount += MAX_OVERDRAFT;
            account.OverdraftAmount = 0;
        }

        var result = await _repository.AddAsync(account);
        return _mapper.Map<AccountDto>(result);
    }

    public async Task UpdateAsync(int id, AccountDto dto)
    {
        if (id != dto.Id)
            throw new BadRequestException("El Id proporcionado no coincide");

        var account = await _repository.GetByIdAsync(id);
        if (account == null)
            throw new NotFoundException($"La cuenta con id [{id}] no existe");

        var updated = _mapper.Map<Account>(dto);
        await _repository.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null)
            throw new NotFoundException($"La cuenta con id [{id}] no existe");

        await _repository.RemoveAsync(account);
    }

    public async Task DepositAsync(int id, TransactionDto transaction)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null)
            throw new NotFoundException($"La cuenta con id [{id}] no existe");

        if (account.AccountNumber != transaction.AccountNumber)
            throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transacción");

        account.BalanceAmount += transaction.ValueAmount;

        if (account.AccountType == 'C')
        {
            if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            else
                account.OverdraftAmount = 0;
        }

        await _repository.UpdateAsync(account);
    }

    public async Task WithdrawAsync(int id, TransactionDto transaction)
    {
        var account = await _repository.GetByIdAsync(id);
        if (account == null)
            throw new NotFoundException($"La cuenta con id [{id}] no existe");

        if (transaction.ValueAmount > account.BalanceAmount)
            throw new BadRequestException("Fondos Insuficientes");

        account.BalanceAmount -= transaction.ValueAmount;

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;

        await _repository.UpdateAsync(account);
    }
}
