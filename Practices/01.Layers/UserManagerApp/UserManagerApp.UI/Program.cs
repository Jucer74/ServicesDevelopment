using System;
using UserManagerApp.Entities;
using UserManagerApp.BL;
using UserManagerApp.DAL;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                Console.WriteLine("\nMenú:");
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Editar Usuario");
                Console.WriteLine("4. Buscar Usuario");
                Console.WriteLine("5. Eliminar Usuario");
                Console.WriteLine("6. Salir");
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
                    Console.WriteLine("El Usuario se agrego con éxito!");
                }
                else if (option == "2") 
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
                else if (option == "3") 
                {
                    Console.Write("Ingrese el ID del usuario que quiera editar: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        var userToEdit = userService.GetUserById(userId);

                        if (userToEdit != null)
                        {
                            Console.Write("Nuevo Nombre (dejelo en blanco para mantener el actual): ");
                            string newName = Console.ReadLine();
                            Console.Write("Nuevo Email (dejelo blanco para mantener el actual): ");
                            string newEmail = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(newName))
                                userToEdit.Name = newName;

                            if (!string.IsNullOrWhiteSpace(newEmail))
                                userToEdit.Email = newEmail;

                            Console.WriteLine("Usuario editado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("Usuario no encontrado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                }
                else if (option == "4") 
                {
                    Console.WriteLine("Buscar por:");
                    Console.WriteLine("1. ID");
                    Console.WriteLine("2. Nombre");
                    Console.Write("Seleccione una opción: ");
                    string searchOption = Console.ReadLine();

                    if (searchOption == "1")
                    {
                        Console.Write("Ingrese el ID del usuario: ");
                        if (int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            var user = userService.GetUserById(searchId);
                            if (user != null)
                            {
                                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                            }
                            else
                            {
                                Console.WriteLine("Usuario no encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                    }
                    else if (searchOption == "2")
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
                            Console.WriteLine("No se encontraron usuarios con tal nombre.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida.");
                    }
                }
                else if (option == "5") // Eliminar usuario
                {
                    Console.Write("Ingrese el ID del usuario que quiere eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        bool deleted = userService.DeleteUser(userId);

                        if (deleted)
                        {
                            Console.WriteLine("Usuario eliminado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("Usuario no encontrado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                }
                else if (option == "6") // Salir
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
