// Program.cs
using System;
using UserManagementApp.BL;
using UserManagementApp.DAL;

namespace UserManagementApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                string option = Console.ReadLine();

                if (option == "1")
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
                else if (option == "2")
                {
                    var users = userService.GetUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                    }
                }
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                }
            }
        }
    }
}