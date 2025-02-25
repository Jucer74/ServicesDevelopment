using System;

namespace UserManagerApp.UI
{
    public static class UserInputHandler
    {
        public static int GetValidNumber(string message)
        {
            int number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Por favor, ingrese un número válido.");
            }
        }

        public static string GetNonEmptyString(string message)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }
    }
}