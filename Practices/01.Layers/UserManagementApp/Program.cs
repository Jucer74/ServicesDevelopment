// Program.cs
using System;
using UserManagementApp.BL;
using UserManagementApp.DAL;

// Definimos un namespace para la capa de UI.
namespace UserManagementApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos una instancia de UserService para interactuar con la lógica de negocio.
            UserService userService = new UserService();

            // Bucle infinito para mostrar el menú hasta que el usuario decida salir.
            while (true)
            {
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    // Opción para agregar un usuario.
                    Console.Write("Nombre: ");
                    string name = Console.ReadLine();
                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    // Creamos un nuevo objeto User. El Id se genera contando los usuarios actuales +1.
                    User newUser = new User
                    {
                        Id = userService.GetUsers().Count + 1,
                        Name = name,
                        Email = email
                    };

                    // Llamamos al método de la capa de negocio para agregar el usuario.
                    userService.AddUser(newUser);
                    Console.WriteLine("Usuario agregado con éxito!");
                }
                else if (option == "2")
                {
                    // Opción para listar todos los usuarios.
                    var users = userService.GetUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                    }
                }
                else if (option == "3")
                {
                    // Salir del bucle y terminar la aplicación.
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
