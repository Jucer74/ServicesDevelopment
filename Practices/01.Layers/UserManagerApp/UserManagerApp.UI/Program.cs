using System;
using UserManagementApp.BL;
using UserManagementApp.Entities;

namespace UserManagementApp.UI
{
    class Program
    {
        static UserService userService = new UserService();

        static void Main(string[] args)
        {
            while (true)
            {
                MostrarMenu();
                int opcion = ObtenerOpcionUsuario();

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
                        Console.WriteLine("Opcion no valida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n--- Gestion de Usuarios ---");
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opcion: ");
        }

        static int ObtenerOpcionUsuario()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int opcion))
                {
                    return opcion;
                }
                Console.WriteLine("Entrada invalida. Por favor, ingrese un numero.");
            }
        }

        static void AgregarUsuario()
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
            Console.WriteLine(" Usuario agregado con exito!");
        }

        static void ListarUsuarios()
        {
            var users = userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine(" No hay usuarios registrados.");
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
