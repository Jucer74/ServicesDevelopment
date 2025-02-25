using System;
using UserManagerApp.BL;
using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar usuario");
                Console.WriteLine("2. Mostrar usuarios");
                Console.WriteLine("3. Mostrar un usuario");
                Console.WriteLine("4. Actualizar usuario");
                Console.WriteLine("5. Eliminar usuario");
                Console.WriteLine("6. Salir");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddUser(userService);
                        break;
                    case "2":
                        ShowUsers(userService);
                        break;
                    case "3":
                        ShowUsers(userService);
                        break;
                    case "4":
                        UptdateUser(userService);
                        break;
                    case "5":
                        DeleteUser(userService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        private static void AddUser(UserService userService)
        {
            Console.WriteLine("Ingrese el nombre del usuario:");
            string name = Console.ReadLine();
            Console.WriteLine("Ingrese la edad del usuario:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el correo del usuario:");
            string email = Console.ReadLine();

            User user = new User
            {
                Name = name,
                Age = age,
                Email = email
            };

            userService.AddUser(user);
            Console.WriteLine("Usuario agregado correctamente.");
        }

        private static void ShowUsers(UserService userService)
        {
            var users = userService.GetAllUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }

            Console.WriteLine("Lista de usuarios:");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id} ,Nombre: {user.Name}, Edad: {user.Age}, Email: {user.Email}");
            }
        }

        private static void ShowUser(UserService userService)
        {
            Console.WriteLine("Ingrese el id del usuario:");
            int id = int.Parse(Console.ReadLine());

            var user = userService.GetUserById(id);
            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.WriteLine($"Id: {user.Id} ,Nombre: {user.Name}, Edad: {user.Age}, Email: {user.Email}");
        }

        private static void UptdateUser(UserService userService)
        {
            Console.WriteLine("Ingrese el id del usuario:");
            int id = int.Parse(Console.ReadLine());

            var user = userService.GetUserById(id);
            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.WriteLine("Ingrese el nombre del usuario:");
            string name = Console.ReadLine();
            Console.WriteLine("Ingrese la edad del usuario:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el correo del usuario:");
            string email = Console.ReadLine();

            user.Name = name;
            user.Age = age;
            user.Email = email;

            userService.UpdateUser(user);
            Console.WriteLine("Usuario actualizado correctamente.");
        }

        private static void DeleteUser(UserService userService)
        {
            Console.WriteLine("Ingrese el id del usuario:");
            int id = int.Parse(Console.ReadLine());

            var user = userService.GetUserById(id);
            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            userService.DeleteUser(user);
            Console.WriteLine("Usuario eliminado correctamente.");
        }
    }
}
