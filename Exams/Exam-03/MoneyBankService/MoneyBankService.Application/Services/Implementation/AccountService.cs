using AutoMapper;
using MoneyBankService.Application.DTO;
using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Services.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Application.Common.Interfaces;

namespace MoneyBankService.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        private const decimal MAX_OVERDRAFT = 1000000;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResultDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();

            if (accounts == null || !accounts.Any())
                throw new NotFoundException("No existen cuentas registradas.");

            return _mapper.Map<IEnumerable<AccountResultDto>>(accounts);
        }

        public async Task<AccountResultDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                 throw new NotFoundException("El ID debe ser mayor que cero.");
            }
               

            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
                throw new NotFoundException($"Cuenta [{id}] no encontrada.");

            return _mapper.Map<AccountResultDto>(account);
        }

        public async Task<AccountResultDto?> GetByAccountNumberAsync(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new BadRequestException("El número de cuenta es requerido.");

            var accounts = await _accountRepository.FindAsync(a => a.AccountNumber == accountNumber);

            var account = accounts.FirstOrDefault();
            

            return _mapper.Map<AccountResultDto>(account);
        }

        public async Task<AccountResultDto> CreateAsync(AccountCreateDto dto)
        {
            var existingAccount = await _accountRepository.FindAsync(a => a.AccountNumber == dto.AccountNumber);
            if (existingAccount.Any())
                throw new BadRequestException("Ya existe una cuenta con ese número.");
            if (dto.BalanceAmount <= 0)
                throw new BadRequestException("El Balance debe ser mayor a cero.");

            var entity = _mapper.Map<Account>(dto);

            if (entity.AccountType == 'A')
            {
                entity.OverdraftAmount = 0;
            }
            else if (entity.AccountType == 'C')
            {
                entity.BalanceAmount += MAX_OVERDRAFT;
                entity.OverdraftAmount = 0;
            }

            await _accountRepository.AddAsync(entity);

            var created = await _accountRepository.GetByIdAsync(entity.Id);
            if (created == null)
                throw new InternalServerErrorException("Error al recuperar la cuenta recién creada.");


            return _mapper.Map<AccountResultDto>(created);
        }

        public async Task UpdateAsync(int id, AccountResultDto dto)
        {
            

            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            var existing = await _accountRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"Cuenta [{id}] no encontrada.");

            _mapper.Map(dto, existing);
            await _accountRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido para eliminar cuenta.");

            var existing = await _accountRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"Cuenta [{id}] no encontrada.");

            await _accountRepository.RemoveAsync(existing);
        }

        public async Task DepositAsync(int id, TransactionCreateDto transaction)
        {
            if (transaction.ValueAmount <= 0)
                throw new BadRequestException("El valor del depósito debe ser mayor a cero.");

            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException($"Cuenta no existe por {id}");
            }
            if (account.AccountNumber != transaction.AccountNumber)
            {
                throw new BadRequestException("Cuenta no válida.");
            }
                

            account.BalanceAmount += transaction.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                else
                    account.OverdraftAmount = 0;
            }

            await _accountRepository.UpdateAsync(account);
        }

        public async Task WithdrawAsync(int id, TransactionCreateDto transaction)
        {
            if (transaction.ValueAmount <= 0)
                throw new BadRequestException("El valor del retiro debe ser mayor a cero.");

            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new NotFoundException("Cuenta no encontrada.");

            if (account.AccountNumber != transaction.AccountNumber)
                throw new BadRequestException("Cuenta no válida.");

            
            decimal totalDisponible = account.AccountType == 'C'
                ? account.BalanceAmount + account.OverdraftAmount
                : account.BalanceAmount;

            if (transaction.ValueAmount > totalDisponible)
                throw new BadRequestException("Fondos Insuficientes.");

           
            account.BalanceAmount -= transaction.ValueAmount;

           
            if (account.AccountType == 'C')
            {
                if (account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
            }

            await _accountRepository.UpdateAsync(account);
        }



    }
}
