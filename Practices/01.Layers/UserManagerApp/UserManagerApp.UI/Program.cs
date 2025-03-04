// Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI{
    
    class Program{
        
        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                DisplayMenu();
                string option = Console.ReadLine();

                if (InputHandler(option) == false)
                {
                    Console.WriteLine("Please enter a number!");
                    continue;
                }
                if (option == "1")
                {
                    AddUser(userService);
                }
                else if (option == "2")
                {
                    DisplayUsers(userService);
                }
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option.");
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. Exit");
            Console.Write("Enter an option: ");
        }

        static bool InputHandler(string input){
            int number;
            while (true)
            {
                if (int.TryParse(input, out number))
                {
                    return true;
                }
                return false;
            }
        }

        static void AddUser(UserService userService)
        {
            try
            {
                Console.Write("Username: ");
                string name = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                    
                User newUser = new User
                {
                    Id = userService.GetUsers().Count + 1,
                    Name = name,
                    Email = email
                };

                userService.AddUser(newUser);
                Console.WriteLine("User added successfully");
            }

            catch (Exception ex)
            {
                Console.Write("Error" + ex.Message);
            }
        }

        static void DisplayUsers(UserService userService)
        {
            var users = userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }
    }
}