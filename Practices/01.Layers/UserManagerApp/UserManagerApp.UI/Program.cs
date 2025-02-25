using System;
using UserManagerApp.BL;
using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        private static UserService userService = new UserService();

        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                int option = GetValidOption();

                switch (option)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        ListUsers();
                        break;
                    case 3:
                        Console.WriteLine("Leaving...");
                        Console.WriteLine("App closed successfully.");
                        return;
                    default:
                        Console.WriteLine("Option invalid.");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n1. Add User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
        }

        private static int GetValidOption()
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                return option;
            }
            Console.WriteLine("Input invalid. please, type a number.");
            return 0;
        }

        private static void AddUser()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            User newUser = new User
            {
                Id = userService.GetUsers().Count + 1,
                Name = name,
                Email = email
            };

            userService.AddUser(newUser);
            Console.WriteLine("User added sucesfully!");
        }

        private static void ListUsers()
        {
            var users = userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
            }
        }
    }
}
