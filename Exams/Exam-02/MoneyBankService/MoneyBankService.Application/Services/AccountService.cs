using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyBankService.Application.Services
{
    // Este es el servicio de cuentas bancarias
    public class AccountService : IAccountService
    {
        // Repositorio de cuentas bancarias
        private readonly IAccountRepository _accountRepository;

        // Límite de sobregiro máximo
        private const int MAX_OVERDRAFT = 1_000_000;

        // Constructor que recibe el repositorio de cuentas
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // Método para crear una cuenta bancaria
        public async Task<Account> CreateAccount(Account account)
        {
            // Si es una cuenta de crédito, se ajusta el saldo agregando el sobregiro máximo
            if (account.AccountType == 'C')
            {
                account.BalanceAmount += MAX_OVERDRAFT;
            }

            // Comprueba si la cuenta ya existe
            await CheckAccountExists(account.AccountNumber);

            // Agrega la cuenta al repositorio
            return await _accountRepository.AddAsync(account);
        }

        // Método para eliminar una cuenta bancaria por su ID
        public async Task DeleteAccount(int id)
        {
            // Obtiene la cuenta original por su ID
            var original = await GetAccountById(id);

            // Elimina la cuenta del repositorio
            await _accountRepository.RemoveAsync(original);
        }

        // Método para obtener una cuenta bancaria por su ID
        public async Task<Account> GetAccountById(int id)
        {
            // Obtiene la cuenta por su ID
            var account = await _accountRepository.GetByIdAsync(id);

            // Si la cuenta no existe, lanza una excepción
            if (account is null)
            {
                throw new NotFoundException($"Account with Id={id} Not Found");
            }

            return account;
        }

        // Método para obtener todas las cuentas bancarias
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            // Obtiene todas las cuentas
            return await _accountRepository.GetAllAsync();
        }

        // Método para actualizar una cuenta bancaria por su ID
        public async Task<Account> UpdateAccount(int id, Account account)
        {
            // Comprueba si el ID coincide con el de la cuenta proporcionada
            if (id != account.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Account.Id [{account.Id}]");
            }

            // Obtiene la cuenta original por su ID
            var original = await GetAccountById(id);

            // Comprueba si el nuevo número de cuenta ya existe
            await CheckAccountNumberExists(account.AccountNumber, original.AccountNumber);

            // Actualiza la cuenta en el repositorio
            return await _accountRepository.UpdateAsync(account);
        }

        // Método para realizar un depósito en una cuenta bancaria
        public async Task Deposit(int id, TransactionDto transactionDto)
        {
            // Obtiene la cuenta por su ID
            var account = await GetAccountById(id);

            // Valida la transacción
            await ValidateTransaction(account, transactionDto);

            // Aumenta el saldo de la cuenta con el valor de la transacción
            account.BalanceAmount += transactionDto.ValueAmount;

            // Actualiza el sobregiro si es una cuenta de crédito
            UpdateOverdraft(account);

            // Actualiza la cuenta en el repositorio
            await _accountRepository.UpdateAsync(account);
        }

        // Método para realizar un retiro de una cuenta bancaria
        public async Task Withdraw(int id, TransactionDto transactionDto)
        {
            // Obtiene la cuenta por su ID
            var account = await GetAccountById(id);

            // Comprueba si el ID coincide con el de la transacción proporcionada
            if (id != transactionDto.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Transaction.Id [{transactionDto.Id}]");
            }

            // Valida la transacción
            await ValidateTransaction(account, transactionDto);

            // Disminuye el saldo de la cuenta con el valor de la transacción
            account.BalanceAmount -= transactionDto.ValueAmount;

            // Actualiza el sobregiro si es una cuenta de crédito y el saldo es menor al límite
            UpdateOverdraft(account);

            // Actualiza la cuenta en el repositorio
            await _accountRepository.UpdateAsync(account);
        }

        // Método para encontrar cuentas bancarias por número de cuenta
        public async Task<IEnumerable<Account>> FindAccountsByAccountNumber(string accountNumber)
        {
            // Busca cuentas en el repositorio por número de cuenta
            var accounts = await _accountRepository.FindAsync(a => a.AccountNumber == accountNumber);

            // Si no se encuentran cuentas, lanza una excepción
            if (!accounts.Any())
            {
                throw new NotFoundException($"Accounts not found");
            }

            return accounts;
        }

        // Método privado para comprobar si una cuenta ya existe
        private async Task CheckAccountExists(string accountNumber)
        {
            // Busca cuentas en el repositorio por número de cuenta
            var accounts = await _accountRepository.FindAsync(a => a.AccountNumber == accountNumber);

            // Si se encuentran cuentas, lanza una excepción
            if (accounts.Any())
            {
                throw new BadRequestException($"Account Number [{accountNumber}] already exists");
            }
        }

        // Método privado para comprobar si un nuevo número de cuenta ya existe
        private async Task CheckAccountNumberExists(string newAccountNumber, string originalAccountNumber)
        {
            // Comprueba si el nuevo número de cuenta es diferente al original
            if (newAccountNumber != originalAccountNumber)
            {
                // Si es diferente, verifica si ya existe
                await CheckAccountExists(newAccountNumber);
            }
        }

        // Método privado para validar una transacción
        private async Task ValidateTransaction(Account account, TransactionDto transactionDto)
        {
            // Comprueba si el número de cuenta de la transacción coincide con el de la cuenta
            if (account.AccountNumber != transactionDto.AccountNumber)
            {
                throw new BadRequestException($"Account Number [{transactionDto.AccountNumber}] is different from Account.AccountNumber [{account.AccountNumber}]");
            }

            // Comprueba si el saldo es suficiente para la transacción
            if (account.BalanceAmount < transactionDto.ValueAmount)
            {
                throw new BadRequestException($"Insufficient funds");
            }
        }

        // Método privado para actualizar el sobregiro
        private void UpdateOverdraft(Account account)
        {
            // Actualiza el sobregiro si es una cuenta de crédito y el saldo es menor al límite
            if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
            else
            {
                account.OverdraftAmount = 0;
            }
        }
    }
}
