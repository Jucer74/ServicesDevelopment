// UserManagerApp.UI/Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static UserService userService = new UserService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        ListUsers();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void AddUser()
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

        static void ListUsers()
        {
            var users = userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }
    }
}