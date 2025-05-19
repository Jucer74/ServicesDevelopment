using AutoMapper;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
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

        public async Task<AccountDto?> GetByIdAsync(Guid id)
        {
            var account = await _repository.GetByIdAsync(id);
            return account == null ? null : _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> CreateAsync(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<AccountDto>(created);
        }

        public async Task<bool> UpdateAsync(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
