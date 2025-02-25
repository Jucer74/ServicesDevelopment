using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                MostrarMenu();
                int opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        AgregarUsuario(userService);
                        break;
                    case 2:
                        ListarUsuarios(userService);
                        break;
                    case 3:
                        Console.WriteLine("Saliendo de la aplicación...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        // Muestra el menú de opciones
        static void MostrarMenu()
        {
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        // Lee la opción ingresada y valida que sea un número
        static int LeerOpcion()
        {
            int opcion;
            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese solo números.");
                Console.Write("Seleccione una opción: ");
            }
            return opcion;
        }

        // Función para agregar un usuario, con validación de email
        static void AgregarUsuario(UserService userService)
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine();

            // Lee y valida el email
            string email = LeerEmail();
            if (!ValidarEmail(email))
            {
                Console.WriteLine("El correo electrónico debe contener '@'. Usuario no agregado.");
                return;
            }

            // Se asigna un ID en base al conteo actual de usuarios
            int id = userService.GetUsers().Count + 1;
            User newUser = new User
            {
                Id = id,
                Name = name,
                Email = email
            };

            userService.AddUser(newUser);
            Console.WriteLine("Usuario agregado con éxito!");
        }

        // Función que solicita el email
        static string LeerEmail()
        {
            Console.Write("Email: ");
            return Console.ReadLine();
        }

        // Validación básica del email (verifica que contenga '@')
        static bool ValidarEmail(string email)
        {
            return email.Contains("@");
        }

        // Función para listar los usuarios
        static void ListarUsuarios(UserService userService)
        {
            var users = userService.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                }
            }
        }
    }
}
