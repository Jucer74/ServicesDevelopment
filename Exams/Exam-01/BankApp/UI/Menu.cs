using System;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Services;
using BankApp.Entities;

namespace BankApp.UI
{
    public class Menu
    {
        private BankService bankService = new BankService();

        // Método asíncrono que muestra el menú y procesa las opciones del usuario
        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1- Create Account");
                Console.WriteLine("2- Get Balance Account");
                Console.WriteLine("3- Deposit Account");
                Console.WriteLine("4- Withdrawal Account");
                Console.WriteLine("0- Exit");
                Console.Write("Select an option: ");
                
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        await CreateAccountUIAsync();
                        break;
                    case "2":
                        await GetBalanceUIAsync();
                        break;
                    case "3":
                        await DepositAccountUIAsync();
                        break;
                    case "4":
                        await WithdrawalAccountUIAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Método asíncrono para crear una cuenta con validaciones
        private async Task CreateAccountUIAsync()
        {
            // 1. Validar el tipo de cuenta
            Console.Write("Account Type (1-Saving, 2-Checking): ");
            string typeInput = Console.ReadLine();
            if (!int.TryParse(typeInput, out int type) || (type != 1 && type != 2))
            {
                Console.WriteLine("Invalid account type.");
                Console.ReadKey();
                return;
            }
            
            // 2. Validar el número de cuenta (exactamente 10 dígitos)
            string accountNumber;
            while (true)
            {
                Console.Write("Account Number: ");
                accountNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(accountNumber) ||
                    accountNumber.Length != 10 ||
                    !accountNumber.All(char.IsDigit))
                {
                    Console.WriteLine("Account number must have exactly 10 digits.");
                }
                else
                {
                    break;
                }
            }
            
            // 3. Validar el nombre del propietario (no vacío y máximo 50 caracteres)
            string accountOwner;
            while (true)
            {
                Console.Write("Account Owner: ");
                accountOwner = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                {
                    Console.WriteLine("Account Owner is required and must be at most 50 characters.");
                }
                else
                {
                    break;
                }
            }
            
            // 4. Validar el monto inicial (numérico y mayor que cero)
            decimal initialBalance;
            while (true)
            {
                Console.Write("Balance Amount: ");
                string balanceInput = Console.ReadLine();
                if (!decimal.TryParse(balanceInput, out initialBalance) || initialBalance <= 0)
                {
                    Console.WriteLine("Amount is required and must be numeric and greater than zero.");
                }
                else
                {
                    break;
                }
            }
            
            // 5. Crear la cuenta usando la entidad correspondiente
            IBankAccount account;
            if (type == 1)
                account = new SavingAccount(accountNumber, accountOwner, initialBalance);
            else
                account = new CheckingAccount(accountNumber, accountOwner, initialBalance);
            
            // 6. Intentar agregar la cuenta vía json‑server
            bool created = await bankService.CreateAccountAsync(account);
            if (created)
                Console.WriteLine("Account Created.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Método asíncrono para mostrar el balance de una cuenta
        private async Task GetBalanceUIAsync()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            await bankService.GetBalanceAccountAsync(accountNumber);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Método asíncrono para realizar un depósito
        private async Task DepositAccountUIAsync()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Deposit Amount: ");
            string depositInput = Console.ReadLine();
            if (!decimal.TryParse(depositInput, out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                Console.ReadKey();
                return;
            }
            await bankService.DepositAccountAsync(accountNumber, amount);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Método asíncrono para realizar un retiro
        private async Task WithdrawalAccountUIAsync()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Withdrawal Amount: ");
            string withdrawalInput = Console.ReadLine();
            if (!decimal.TryParse(withdrawalInput, out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                Console.ReadKey();
                return;
            }
            await bankService.WithdrawalAccountAsync(accountNumber, amount);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
