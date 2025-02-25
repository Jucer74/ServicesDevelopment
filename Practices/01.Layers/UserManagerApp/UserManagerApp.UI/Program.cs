using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            Menu menu = new Menu(userService);
            menu.Run();
        }
    }

    class Menu
    {
        private readonly UserService _userService;

        public Menu(UserService userService)
        {
            _userService = userService;
        }

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string option = GetValidOption();

                if (option == "1")
                {
                    AddUser();
                }
                else if (option == "2")
                {
                    ListUsers();
                }
                else if (option == "3")
                {
                    Console.WriteLine("Saliendo del programa");
                    break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        private static string GetValidOption()
        {
            string input;
            int result;
        
            while (true)
            {

                input = Console.ReadLine();
        
                if (int.TryParse(input, out result) && result >= 1 && result <= 3)
                {
                    return input;
                }
                
                Console.WriteLine("Opción inválida. Intente nuevamente.");
                DisplayMenu();
            }
        }

        private void AddUser()
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            foreach (var user in _userService.GetUsers())
            {
                if (user.Name == name && user.Email == email)
                {
                    Console.WriteLine("El usuario ya existe");
                    return;
                }
            }

            User newUser = new User
            {
                Id = _userService.GetUsers().Count + 1,
                Name = name,
                Email = email
            };

            _userService.AddUser(newUser);
            Console.WriteLine("Usuario añadido");
        }

        private void ListUsers()
        {
            var users = _userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }

    }
}