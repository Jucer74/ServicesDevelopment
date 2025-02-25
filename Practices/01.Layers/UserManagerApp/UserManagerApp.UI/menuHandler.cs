
using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    public class MenuHandler
    {
        private readonly UserService _userService;

        public MenuHandler(UserService userService)
        {
            _userService = userService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Salir");

                int option = UserInputHandler.GetValidNumber("Seleccione una opción: ");
                HandleOption(option);
            }
        }

        private void HandleOption(int option)
        {
            switch (option)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    ListUsers();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private void AddUser()
        {
            string name = UserInputHandler.GetNonEmptyString("Nombre: ");
            string email = UserInputHandler.GetNonEmptyString("Email: ");
            
            User newUser = new User
            {
                Id = _userService.GetUsers().Count + 1,
                Name = name,
                Email = email
            };

            _userService.AddUser(newUser);
            Console.WriteLine("Usuario agregado con éxito!");
        }

        private void ListUsers()
        {
            var users = _userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }
    }
}