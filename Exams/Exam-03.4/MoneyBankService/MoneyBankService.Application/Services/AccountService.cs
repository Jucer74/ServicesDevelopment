using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions; 

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
        private const decimal MAX_OVERDRAFT = 1_000_000; //

        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }




        // 

        public async Task<AccountDto> CreateAccountAsync(AccountDto accountDto)
        {
            
            if (accountDto.BalanceAmount <= 0)
            {
                throw new BadRequestException("El Balance debe ser mayor a cero para aperturar la Cuenta."); //
            }

            
            var existingAccounts = await _accountRepository.FindAsync(acc => acc.AccountNumber == accountDto.AccountNumber);
            if (existingAccounts.Any())
            {
                throw new BadRequestException($"La Cuenta [{accountDto.AccountNumber}] ya Existe."); //
            }

            var account = _mapper.Map<Account>(accountDto);

          
            account.CreationDate = DateTime.Now; //


            if (account.AccountType == 'C') 
            {
                
                account.BalanceAmount += MAX_OVERDRAFT;
               
                account.OverdraftAmount = 0; 
            }
            else 
            {
                
                account.OverdraftAmount = 0;
            }

           
            var createdAccountEntity = await _accountRepository.AddAsync(account);

            
            return _mapper.Map<AccountDto>(createdAccountEntity);
        }

       

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsByAccountNumberAsync(string accountNumber)
        {
           
            var accounts = await _accountRepository.FindAsync(acc => acc.AccountNumber == accountNumber);
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int id) 
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {

                return null;
            }
            return _mapper.Map<AccountDto>(account);
        }


        //3 

        public async Task<bool> DeleteAccountAsync(int id)
        {
            
            var accountToDelete = await _accountRepository.GetByIdAsync(id);
            if (accountToDelete == null)
            {
               
                throw new NotFoundException($"La Cuenta con Id [{id}] No Existe y no puede ser eliminada.");
            }

         
            await _accountRepository.RemoveAsync(accountToDelete);

            return true; 
        }



        //4


       

        public async Task<bool> DepositAsync(int id, TransactionDto transactionDto)
        {
            
            if (id != transactionDto.Id)
            {
                throw new BadRequestException("El Id de la ruta no coincide con el Id del cuerpo de la transacción."); //
            }

            
            if (transactionDto.ValueAmount <= 0)
            {
                throw new BadRequestException("El valor del depósito debe ser mayor a cero.");
            }

            
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException($"La Cuenta con Id [{id}] No Existe."); 
            }

            
            if (account.AccountNumber != transactionDto.AccountNumber)
            {
                throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transaccion."); 
            }

            
            account.BalanceAmount += transactionDto.ValueAmount; 

            if (account.AccountType == 'C') 
            {
                
                if (account.BalanceAmount < MAX_OVERDRAFT)
                {
                 
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
                else
                {
                    
                    account.OverdraftAmount = 0;
                }
            }
     
            await _accountRepository.UpdateAsync(account);

            return true;
        }








        //2

        public async Task<AccountDto> UpdateAccountAsync(int id, AccountDto accountDto)
        {
            if (id != accountDto.Id)
            {
                throw new BadRequestException("El Id de la ruta no coincide con el Id del cuerpo de la solicitud.");
            }

            var existingAccount = await _accountRepository.GetByIdAsync(id);
            if (existingAccount == null)
            {
                throw new NotFoundException($"La Cuenta con Id [{id}] No Existe y no puede ser actualizada.");
            }


            var accountWithDtoNumberExists = await _accountRepository.FindAsync(acc => acc.AccountNumber == accountDto.AccountNumber);
            if (!accountWithDtoNumberExists.Any()) 
            {
                
                throw new BadRequestException($"El número de cuenta [{accountDto.AccountNumber}] proporcionado para la actualización no corresponde a una cuenta existente en el sistema.");
            }

          
            if (existingAccount.AccountNumber != accountDto.AccountNumber)
            {
                
                var otherAccountWithNewNumber = accountWithDtoNumberExists.FirstOrDefault(acc => acc.Id != existingAccount.Id);
                if (otherAccountWithNewNumber != null)
                {
                    throw new BadRequestException($"El número de cuenta [{accountDto.AccountNumber}] ya está asignado a otra cuenta (Id: {otherAccountWithNewNumber.Id}) y no puede ser duplicado.");
                }
            }

            
            existingAccount.OwnerName = accountDto.OwnerName;
            existingAccount.AccountNumber = accountDto.AccountNumber; 
            existingAccount.AccountType = accountDto.AccountType;     

            
            existingAccount.BalanceAmount = accountDto.BalanceAmount;
            existingAccount.OverdraftAmount = accountDto.OverdraftAmount;

            var updatedAccountEntity = await _accountRepository.UpdateAsync(existingAccount);
            return _mapper.Map<AccountDto>(updatedAccountEntity);
        }




        //5




        public async Task<bool> WithdrawAsync(int id, TransactionDto transactionDto)
        {
            if (id != transactionDto.Id)
            {
                throw new BadRequestException("El Id de la ruta no coincide con el Id del cuerpo de la transacción.");
            }

            if (transactionDto.ValueAmount <= 0)
            {
                throw new BadRequestException("El valor del retiro debe ser mayor a cero.");
            }

            var account = await _accountRepository.GetByIdAsync(id); // Obtiene la cuenta por el 'id' de la URL
            if (account == null)
            {
                throw new NotFoundException($"La Cuenta con Id [{id}] No Existe.");
            }

            // ***** VALIDACIÓN AÑADIDA *****
            // Compara el número de cuenta de la entidad encontrada con el número de cuenta del DTO de la transacción
            if (account.AccountNumber != transactionDto.AccountNumber)
            {
                throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transaccion.");
            }
            // ****************************

            if (account.BalanceAmount < transactionDto.ValueAmount)
            {
                throw new BadRequestException("Fondos Insuficientes.");
            }

            account.BalanceAmount -= transactionDto.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
                else
                {
                    account.OverdraftAmount = 0;
                }
            }

            await _accountRepository.UpdateAsync(account);
            return true;
        }


    }
}