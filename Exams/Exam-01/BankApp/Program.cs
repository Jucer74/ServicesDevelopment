using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        BankService bankService = new BankService();

        while (true)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1- Create account ");
            Console.WriteLine("2- Get Balance");
            Console.WriteLine("3- Deposit");
            Console.WriteLine("4- Withdraw Amount");
            Console.WriteLine("0- Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Número de cuenta (10 dígitos): ");
                    string accNum = Console.ReadLine();
                    Console.Write("Titular: ");
                    string owner = Console.ReadLine();
                    Console.Write("Saldo inicial: ");
                    decimal balance = decimal.Parse(Console.ReadLine());
                    Console.Write("Tipo (1- Ahorros, 2- Corriente): ");
                    AccountType type = (AccountType)int.Parse(Console.ReadLine());

                    IBankAccount account = type == AccountType.Saving ?
                        new SavingAccount(accNum, owner, balance) :
                        new CheckingAccount(accNum, owner, balance);

                    await bankService.CreateAccount(account);
                    break;

                case "2":
                    Console.Write("Número de cuenta: ");
                    await bankService.GetBalance(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("Número de cuenta: ");
                    string acc = Console.ReadLine();
                    Console.Write("Monto a depositar: ");
                    await bankService.DepositAccount(acc, decimal.Parse(Console.ReadLine()));
                    break;

                case "4":
                    Console.Write("Número de cuenta: ");
                    acc = Console.ReadLine();
                    Console.Write("Monto a retirar: ");
                    await bankService.WithdrawalAccount(acc, decimal.Parse(Console.ReadLine()));
                    break;

                case "0":
                    return;
            }
        }
    }
}
