using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main()
        {
            UserService userService = new UserService();

            while (true)
            {
                Console.WriteLine("\n1. Agregar Usuario");
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
                        Name = name,
                        Email = email
                    };

                    userService.AddUser(newUser);
                    Console.WriteLine(" Usuario agregado con éxito!");
                }
                else if (option == "2")
                {
                    var users = userService.GetUsers();
                    if (users.Count == 0)
                    {
                        Console.WriteLine(" No hay usuarios registrados.");
                    }
                    else
                    {
                        foreach (var user in users)
                        {
                            Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                        }
                    }
                }
                else if (option == "3")
                {
                    Console.WriteLine(" Saliendo...");
                    break;
                }
                else
                {
                    Console.WriteLine(" Opción no válida.");
                }
            }
        }
    }
}
