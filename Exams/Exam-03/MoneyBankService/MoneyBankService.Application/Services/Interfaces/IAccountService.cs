using MoneyBankService.Application.DTO;

namespace MoneyBankService.Application.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<AccountResultDto>> GetAllAsync();
        public Task<AccountResultDto?> GetByIdAsync(int id);
        public Task<AccountResultDto?> GetByAccountNumberAsync(string accountNumber);
        public Task<AccountResultDto> CreateAsync(AccountCreateDto dto);
        public Task UpdateAsync(int id, AccountResultDto dto); 
        public Task DeleteAsync(int id);
        public Task DepositAsync(int id, TransactionCreateDto transaction);
        public Task WithdrawAsync(int id, TransactionCreateDto transaction);
    }
}
