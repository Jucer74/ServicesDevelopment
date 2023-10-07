using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    // Vars
    private readonly IAccountRepository _accountRepository;

    // Const 
    private const int MaxOverdraft = 1_000_000;

    // Constructor
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    // Add Account
    public async Task<Accounts> AddAccountAsync(Accounts account)
    {
        // Validate account
        if (account == null)
        {
            throw new BadRequestException("Account is required.");
        }

        // Validate account number
        if (string.IsNullOrWhiteSpace(account.AccountNumber))
        {
            throw new BadRequestException("AccountNumber is required.");
        }

        // Validate owner name
        if (string.IsNullOrWhiteSpace(account.OwnerName))
        {
            throw new BadRequestException("OwnerName is required.");
        }

        // Validate account type
        switch (account.AccountType)
        {
            case 'C':
                // Account checking: Calculate initial balance as the deposit
                account.BalanceAmount += MaxOverdraft;
                break;
            case 'A':
                // Account savings: Calculate initial balance as the deposit
                break;
            default:
                throw new BadRequestException("Invalid account type.");
        }

        // Validate balance amount
        if (account.BalanceAmount <= 0)
        {
            throw new BadRequestException("BalanceAmount must be greater than 0.");
        }

        // Check if account already exists
        var accountExist = await _accountRepository.FindAsync(a => a.AccountNumber == account.AccountNumber);
        if (accountExist.Any())
        {
            throw new BadRequestException($"Account with number {account.AccountNumber} already exists.");
        }

        // Add account
        var addedAccount = await _accountRepository.AddAsync(account);

        // Return added account
        return addedAccount;
    }

    // Get Account by Account Number
    public async Task<Accounts?> GetAccountAsync(string accountNumber)
    {
        // Get account by account number
        var account = await _accountRepository.FindAsync(a => a.AccountNumber == accountNumber);

        // If account is null
        if (account == null)
        {
            throw new NotFoundException($"Account with number {accountNumber} not found.");
        }

        // If account is not exist
        var accountsEnumerable = account as Accounts[] ?? account.ToArray();
        if (!accountsEnumerable.Any())
        {
            throw new NotFoundException($"Account with number {accountNumber} not found.");
        }

        // Return account
        return accountsEnumerable.FirstOrDefault();
    }

    // Get Account by Id
    public async Task<Accounts> GetAccountByIdAsync(int id)
    {
        // Get account by id
        var account = await _accountRepository.GetByIdAsync(id);

        // If account is null
        if (account == null)
        {
            throw new NotFoundException($"Account with id {id} not found.");
        }

        // Return account
        return account;
    }

    // Get All Accounts
    public async Task<IEnumerable<Accounts>> GetAllAccountsAsync()
    {
        // Get all accounts
        var accounts = await _accountRepository.GetAllAsync();

        // If accounts is null
        if (accounts == null)
        {
            throw new NotFoundException("Accounts not found.");
        }

        // Return accounts
        return accounts;
    }

    // Update Account
    public async Task<Accounts> UpdateAccountAsync(int id, Accounts account)
    {
        // If account is null
        if (account == null)
        {
            throw new BadRequestException("Account is required.");
        }

        // If account type is valid or not
        if (id != account.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Account.Id [{account.Id}]");
        }

        // Get account by id
        var accountById = await _accountRepository.GetByIdAsync(id);

        // If account is null
        if (accountById == null)
        {
            throw new NotFoundException($"Account with id {id} not found.");
        }

        // Update account
        accountById.AccountNumber = account.AccountNumber;
        accountById.AccountType = account.AccountType;
        accountById.BalanceAmount = account.BalanceAmount;
        accountById.OverdraftAmount = account.OverdraftAmount;

        // Update account
        var updatedAccount = await _accountRepository.UpdateAsync(accountById);

        // Return updated account
        return updatedAccount;
    }

    // Delete Account
    public async Task<Accounts> DeleteAccountAsync(int id)
    {
        // Get account by id
        var account = await _accountRepository.GetByIdAsync(id);

        // If account is null
        if (account == null)
        {
            throw new NotFoundException($"Account with id {id} not found.");
        }

        // Delete account
        await _accountRepository.RemoveAsync(account);

        // Return deleted account
        return account;
    }

    // Deposit
    public async Task<Accounts> DepositAsync(int id, Transactions transaction)
    {
        // Get account by id
        var account = await _accountRepository.GetByIdAsync(id);

        // If account is null or account number mismatch
        if (account == null || account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("Invalid account or account number mismatch.");
        }

        // Add value amount to balance amount
        account.BalanceAmount += transaction.ValueAmount;

        // If account type is not 'C'
        if (account.AccountType != 'C') return await _accountRepository.UpdateAsync(account);

        // If overdraft amount is greater than 0 and balance amount is less than max overdraft
        if (account is { OverdraftAmount: > 0, BalanceAmount: < MaxOverdraft })
        {
            account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
        }
        else
        {
            account.OverdraftAmount = 0;
        }

        return await _accountRepository.UpdateAsync(account);
    }

    // Withdraw
    public async Task<Accounts> WithdrawAsync(int id, Transactions transaction)
    {
        // Get account by id
        var account = await _accountRepository.GetByIdAsync(id);

        // If account is null or account number mismatch
        if (account == null || account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("Invalid account or account number mismatch.");
        }

        // If value amount is greater than balance amount
        if (transaction.ValueAmount > account.BalanceAmount)
        {
            throw new BadRequestException("Insufficient funds");
        }

        // Subtract value amount to balance amount
        account.BalanceAmount -= transaction.ValueAmount;

        // If account type is not 'C'
        if (account.AccountType != 'C') return await _accountRepository.UpdateAsync(account);

        // If overdraft amount is greater than 0 and balance amount is less than max overdraft
        if (account is { OverdraftAmount: > 0, BalanceAmount: < MaxOverdraft })
        {
            account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
        }

        return await _accountRepository.UpdateAsync(account);
    }
}