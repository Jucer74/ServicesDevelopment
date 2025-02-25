using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Se crea la instancia de UserService y se envia a MenuManager
            UserService userService = new UserService();
            InputValidator inputValidator = new InputValidator();
            MenuManager menuManager = new MenuManager(userService, inputValidator);
            menuManager.RunMenu();
        }
    }

    // Esta clase maneja la logica del menu
    class MenuManager
    {
        private readonly UserService userService;
        private readonly InputValidator validator;

        public MenuManager(UserService userService, InputValidator inputValidator)
        {
            this.userService = userService;
            this.validator = inputValidator;
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string input = ReadUserInput();
                int option = validator.ValidateOption(input); 

                if (option == 1)
                {
                    AddUser();
                }
                else if (option == 2)
                {
                    ListUsers();
                }
                else if (option == 3)
                {
                    Exit();
                    break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        private string ReadUserInput()
        {
            return Console.ReadLine();
        }

        private void AddUser()
        {
            Console.Write("Nombre: ");
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
            Console.WriteLine("Usuario agregado con éxito!");
        }

        private void ListUsers()
        {
            var users = userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }

        private void Exit()
        {
            Console.WriteLine("Hasta la próxima.");
        }
    }

    // Esta clase maneja la validación del numero 
    class InputValidator
    {
        public int ValidateOption(string input)
        {
            while (true)
            {
                if (int.TryParse(input, out int option) && option >= 1 && option <= 3)
                {
                    return option;
                }
                Console.WriteLine("Entrada no válida. Por favor, ingrese un número entre 1 y 3.");
                Console.Write("Seleccione una opción: ");
                input = Console.ReadLine(); 
            }
        }
    }
}
