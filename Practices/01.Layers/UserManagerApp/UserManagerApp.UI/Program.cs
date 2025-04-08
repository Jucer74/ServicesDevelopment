// See https://aka.ms/new-console-template for more information
// Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagementApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();

            while (true)
            {
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Listar Usuarios");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();

                if (isInputValid(option) == false)
                {
                    Console.WriteLine("Se debe ingresar un número.");
                    continue;

                }

                if (option == "1")
                {
                    addUser(userService);
                }
                else if (option == "2")
                {
                    listUsers(userService);
                }
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                }
            }
        }

        static public void addUser(UserService userService) {

            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            User newUser = new User
            {
                Id = null,
                Name = name,
                Email = email
            };

            userService.AddUser(newUser);

            Console.WriteLine("Usuario agregado con éxito!");

        }

        static public void listUsers(UserService userService) {

            var users = userService.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
            }
        }

        static public bool isInputValid(string input) {

            return int.TryParse(input, out _);
        }
    }

}