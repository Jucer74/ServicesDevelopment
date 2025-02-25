using System;
using UserManagerApp.Entities;
using UserManagerApp.BL;
using UserManagerApp.DAL;

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
                        EditarUsuario();
                        break;
                    case 4:
                        BuscarUsuario();
                        break;
                    case 5:
                        EliminarUsuario();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Editar Usuario");
            Console.WriteLine("4. Buscar Usuario");
            Console.WriteLine("5. Eliminar Usuario");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static int LeerOpcion()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int opcion))
                    return opcion;

                Console.WriteLine("Entrada no válida. Ingrese un número.");
                Console.Write("Seleccione una opción: ");
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
            Console.WriteLine("Usuario agregado con éxito!");
        }

        static void ListarUsuarios()
        {
            var users = userService.GetUsers();
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

        static void EditarUsuario()
        {
            Console.Write("Ingrese el ID del usuario que desea editar: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var userToEdit = userService.GetUserById(userId);
            if (userToEdit == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.Write("Nuevo Nombre (dejelo en blanco para mantener el actual): ");
            string newName = Console.ReadLine();
            Console.Write("Nuevo Email (dejelo en blanco para mantener el actual): ");
            string newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
                userToEdit.Name = newName;
            if (!string.IsNullOrWhiteSpace(newEmail))
                userToEdit.Email = newEmail;

            Console.WriteLine("Usuarioo se edito con éxito!");
        }

        static void BuscarUsuario()
        {
            Console.WriteLine("Buscar por:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Nombre");
            Console.Write("Seleccione una opción: ");
            
            int searchOption = LeerOpcion();

            if (searchOption == 1)
            {
                Console.Write("Ingrese el ID del usuario: ");
                if (!int.TryParse(Console.ReadLine(), out int searchId))
                {
                    Console.WriteLine("ID inválido.");
                    return;
                }

                var user = userService.GetUserById(searchId);
                if (user != null)
                    Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                else
                    Console.WriteLine("Usuario no encontrado.");
            }
            else if (searchOption == 2)
            {
                Console.Write("Ingrese el nombre a buscar: ");
                string searchName = Console.ReadLine();
                var users = userService.GetUsersByName(searchName);

                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron usuarios que tengan ese nombre.");
                }
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        static void EliminarUsuario()
        {
            Console.Write("Ingrese el ID del usuario que desea eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            bool eliminado = userService.DeleteUser(userId);
            if (eliminado)
                Console.WriteLine("Usuario eliminado con éxito!");
            else
                Console.WriteLine("Usuario no encontrado.");
        }
    }
}
