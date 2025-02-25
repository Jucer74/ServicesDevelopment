// Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
        }

        static int LeerOpcion()
        {
            int opcionInt;
            while (true)
            {
                Console.Write("Seleccione una opción: ");
                string opcionStr = Console.ReadLine() ?? "";

                if (int.TryParse(opcionStr, out opcionInt))
                {
                    return opcionInt;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                }
            }
        }

        static void CrearUsuario(UserService userService)
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine() ?? "";

            while (true)
            {
                Console.Write("Email: ");
                string email = Console.ReadLine() ?? "";

                if (name != "" && email != "" && email.Contains("@") && email.Contains("."))
                {
                    User newUser = new User
                    {
                        Id = userService.GetUsers().Count + 1,
                        Name = name,
                        Email = email
                    };
                    userService.AddUser(newUser);
                    Console.WriteLine("Usuario agregado con éxito!");
                    break;
                }
                else
                {
                    Console.WriteLine("Correo inválido (Recuerda que debe tener un @ y un .)");
                }
            }
        }

        static void MostrarUsuarios(UserService userService)
        {
            var users = userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }

        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                MostrarMenu();
                int opcion = LeerOpcion();

                if (opcion == 1)
                {
                    CrearUsuario(userService);
                }
                else if (opcion == 2)
                {
                    MostrarUsuarios(userService);
                }
                else if (opcion == 3)
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