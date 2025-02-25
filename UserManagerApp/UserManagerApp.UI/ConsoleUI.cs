namespace UserManagerApp.UI
{
    using System;
    using UserManagerApp.BL;
    using UserManagerApp.Entities;
    using System.Collections.Generic;

    public class ConsoleUI
    {
        private readonly UserManager _userManager;

        public ConsoleUI(UserManager userManager)
        {
            _userManager = userManager;
        }

        public void MostrarMenu()
        {
            Console.WriteLine("\n--- MENÚ ---");
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Salir");
        }

        public int LeerOpcion()
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

        public void CrearUsuario()
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine()?.Trim() ?? "";

            try
            {
                _userManager.Add(name, email);
                Console.WriteLine("Usuario agregado con éxito!");
            }
            catch (ArgumentException ex) // Captura errores de validación
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex) // Captura cualquier otro error inesperado
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }


        public void MostrarUsuarios()
        {
            List<User> users = _userManager.GetAll();
            if (users.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                Console.WriteLine("\n--- LISTA DE USUARIOS ---");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                }
            }
        }

        public void Ejecutar()
        {
            while (true)
            {
                MostrarMenu();
                int opcion = LeerOpcion();

                if (opcion == 1)
                {
                    CrearUsuario();
                }
                else if (opcion == 2)
                {
                    MostrarUsuarios();
                }
                else if (opcion == 3)
                {
                    Console.WriteLine("Saliendo del programa...");
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
