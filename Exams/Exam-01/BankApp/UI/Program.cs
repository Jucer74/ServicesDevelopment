using System;


public class Program
{

    static void Main()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine(@"
\tBanking Operation
-----------------------------------
1. Create Account.
2. Get Balance.
3. Deposit Amount.
4. Withdrawal Amount.
0. Exit.
Select Option: ");

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        int typeOption;
                        Console.Clear();
                        Console.WriteLine(@"
\tCreate Account
-----------------------------------
Select account type (1-Saving | 2-Checking): ");

                        if (int.TryParse(Console.ReadLine(), out typeOption))
                        {

                        }
                        else
                        {
                            Console.WriteLine("Por favor, ingrese un número válido.");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Has seleccionado la Opción 2.");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Has seleccionado la Opción 3.");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Has seleccionado la Opción 4.");
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
            }

            Console.WriteLine("\nPresiona Enter para continuar...");
            Console.ReadLine();
        } while (option != 0);

    }
}