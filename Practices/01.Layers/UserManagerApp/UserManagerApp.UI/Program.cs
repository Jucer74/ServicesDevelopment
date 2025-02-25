using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        private static UserService userService = new UserService();

        static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n1. Add user");
                Console.WriteLine("2. Show users");
                Console.WriteLine("3. Exit");
                Console.Write("Digit a option number: ");

                int option = ValidateInput();

                switch (option)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        ListUsers();
                        break;
                    case 3:
                        Console.WriteLine("Closing app...");
                        return;
                    default:
                        Console.WriteLine("Invalid Option. Try again.");
                        break;
                }
            }
        }

        private static void AddUser()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            User newUser = new User
            {
                Id = GenerateUserId(),
                Name = name,
                Email = email
            };

            userService.AddUser(newUser);
            Console.WriteLine("User Added!!!!");
        }

        private static void ListUsers()
        {
            var users = userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("There is no users.");
                return;
            }

            Console.WriteLine("\nUsers list:");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
            }
        }

        private static int ValidateInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    return number;
                }
                Console.WriteLine("Invalid entry. Please, only numbers.");
            }
        }

        private static int GenerateUserId()
        {
            var users = userService.GetUsers();
            return users.Count == 0 ? 1 : users[^1].Id + 1;
        }
    }
}
