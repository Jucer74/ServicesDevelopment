using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;
using System.Transactions;
using Transaction = MoneyBankService.Domain.Entities.Transaction;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const decimal MAX_OVERDRAFT = 1000000.00M;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAccount(Account account)
    {

        if (account.AccountType == 'A') 
        {
            // El balance ya es el valor inicial proporcionado
        }
        // Si la cuenta es corriente, sumamos el MAX_OVERDRAFT al balance inicial
        else if (account.AccountType == 'C')  
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        return await _accountRepository.AddAsync(account);
    }


    public async Task DeleteAccount(int id)
    {
        var original = await _accountRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _accountRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }

    public async Task<Account> GetAccountById(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account is not null)
        {
            return account;
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }
    public async Task<IEnumerable<Account>> GetAccountsByAccountNumberAsync(string accountNumber)
    {

            var accounts = await _accountRepository.FindAsync(account => account.AccountNumber == accountNumber);
            return accounts;


    }


    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Account> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Team.Id [{account.Id}]");
        }

        var original = await _accountRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _accountRepository.UpdateAsync(account);
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }

    public async Task Deposit(int id, Transaction transaction)
    {
        // Obtenemos la cuenta por su ID
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        // Incrementamos el balance de la cuenta con el valor depositado
        account.BalanceAmount += transaction.ValueAmount;

        // Si es una cuenta corriente
        if (account.AccountType == 'C')
        {
            // Si después del depósito el balance sigue siendo menor al sobregiro máximo
            if (account.BalanceAmount < MAX_OVERDRAFT)
            {
                // Recalculamos el sobregiro
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
            else
            {
                // Si después del depósito, el balance excede el sobregiro máximo
                account.OverdraftAmount = 0;  // Ajustamos el sobregiro a cero
            }
        }

        await _accountRepository.UpdateAsync(account);  // Actualizamos la cuenta en la base de datos
    }



    public async Task Withdrawal(int id, Transaction transaction)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        // Si el monto del retiro es mayor que el balance total (balance + sobregiro)
        if (transaction.ValueAmount > (account.BalanceAmount + MAX_OVERDRAFT - account.OverdraftAmount))
        {
            throw new BadRequestException("Fondos Insuficientes");
        }

        account.BalanceAmount -= transaction.ValueAmount;

        // Si es una cuenta corriente y el balance está por debajo del millón prestado
        if (account.AccountType == 'C' && account.BalanceAmount < MAX_OVERDRAFT)
        {
            // El sobregiro utilizado se calcula como la diferencia entre el MAX_OVERDRAFT y el balance actual
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;

            // Garantizar que el sobregiro no exceda el valor de MAX_OVERDRAFT
            if (account.OverdraftAmount > MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT;
            }
        }

        await _accountRepository.UpdateAsync(account);
    }








}


