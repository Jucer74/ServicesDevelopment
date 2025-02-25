using System;
using System.Collections.Generic;
using UserManagerApp.BL;
using UserManagerApp.Entities;
using UserManagerApp.DAL;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            UserMenu menu = new UserMenu(userService);
            menu.Run();
        }
    }

    class UserMenu
    {
        private readonly UserService _userService;
        private readonly Dictionary<string, Action> _options;

        public UserMenu(UserService userService)
        {
            _userService = userService;
            _options = new Dictionary<string, Action>
            {
                { "1", AddUser },
                { "2", ListUsers },
                { "3", EditUser },
                { "4", DeleteUser },
                { "5", Exit }
            };
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== MENÚ PRINCIPAL =====");
                Console.ResetColor();
                
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Editar Usuario");
                Console.WriteLine("4. Eliminar Usuario");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                
                string option = Console.ReadLine();

                if (_options.ContainsKey(option))
                {
                    Console.Clear();
                    _options[option]();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida, intente de nuevo.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        private void AddUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== Agregar Usuario ===");
            Console.ResetColor();
            
            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            User newUser = new User { Name = name, Email = email };
            _userService.AddUser(newUser);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Usuario agregado con éxito!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void ListUsers()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n=== Lista de Usuarios ===");
            Console.ResetColor();

            var users = _userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id} | Nombre: {user.Name} | Email: {user.Email}");
                }
            }
            Console.ReadKey();
        }

        private void EditUser()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== Editar Usuario ===");
            Console.ResetColor();
            
            Console.Write("Ingrese el ID del usuario a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Nuevo nombre: ");
                string newName = Console.ReadLine();
                Console.Write("Nuevo email: ");
                string newEmail = Console.ReadLine();

                _userService.UpdateUser(new User { Id = id, Name = newName, Email = newEmail });

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Usuario actualizado con éxito!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID inválido, debe ser un número.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        private void DeleteUser()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n=== Eliminar Usuario ===");
            Console.ResetColor();
            
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _userService.DeleteUser(id);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Usuario eliminado con éxito!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID inválido, debe ser un número.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        private void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSaliendo del sistema");
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}
