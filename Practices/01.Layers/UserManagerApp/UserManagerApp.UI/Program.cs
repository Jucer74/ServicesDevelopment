// Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.DAL;
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
                MostrarMenu();
                int opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        AgregarUsuario();
                        break;
                    case 2:
                        ListarUsuarios();
                        break;
                    case 3:
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n--- Gestión de Usuarios ---");
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static int LeerOpcion()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int opcion))
                return opcion;
            
            Console.WriteLine("Entrada no válida. Ingrese un número.");
            return 0; // Retorna 0 para que el switch lo detecte como opción inválida
        }

        static void AgregarUsuario()
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine()?.Trim() ?? "Usuario Desconocido";

            Console.Write("Email: ");
            string email = Console.ReadLine()?.Trim() ?? "email@desconocido.com";

            User newUser = new User
            {
                Id = userService.GetUsers().Count + 1,
                Name = name,
                Email = email
            };

            userService.AddUser(newUser);
            Console.WriteLine("✅ Usuario agregado con éxito.");
        }

        static void ListarUsuarios()
        {
            var users = userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("⚠ No hay usuarios registrados.");
                return;
            }

            Console.WriteLine("\n--- Lista de Usuarios ---");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }
    }
}



