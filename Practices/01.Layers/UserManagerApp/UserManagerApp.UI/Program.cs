using UserManagerApp.BL;
using UserManagerApp.DAL;
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
                Console.WriteLine("\n1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Editar Usuario");
                Console.WriteLine("4. Eliminar Usuario");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    if (option == 5) break;
                    Menu(option,userService);
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
                }
            }
        }

        static void Menu(int option,UserService userService)
        {
             switch (option)
            {
                case 1:
                     AddUser(userService);
                    break;
                case 2:
                    GetUsers(userService);
                    break;
                case 3:
                    UpdateUser(userService);
                    break;
                case 4:
                    DeleteUser(userService);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        static void AddUser(UserService userService)
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

        static void GetUsers(UserService userService)
        {
            var users = userService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }

        static void UpdateUser(UserService userService)
        {
            Console.Write("Ingrese el Id del usuario a editar: ");
            string id = Console.ReadLine();
            Console.Write("Nuevo Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Nuevo Email: ");
            string email = Console.ReadLine();

            User newUser = new User
            {
                Id = int.Parse(id),
                Name = name,
                Email = email
            };
            string message = userService.UpdateUser(newUser);
            Console.Write(message);
        }

        static void DeleteUser(UserService userService)
        {
            Console.Write("\nUsuarios en el sistema:\n");
            GetUsers(userService);
            Console.Write("\nIngrese el Id del usuario a eliminar: ");
            string id = Console.ReadLine();
            userService.DeteleUser(int.Parse(id));
        }


    }
}