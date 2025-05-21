using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Account> CreateAsync(Account account);

    // Elimina una account por su ID.
    Task DeleteAsync(int id);

    // Obtiene todas las accounts.
    Task<IEnumerable<Account>> GetAllAsync();

    // Obtiene una account por su ID.
    Task<Account> GetByIdAsync(int id);

    // Actualiza una account existente por su ID.
    Task<Account> UpdateAsync(int id, Account account);

    Task UpdateValue(int accountId, Transaction transaction, char transactionType);

    Task<List<Account>> GetAccountByAccountNumber(string accountNumber);
}
