// Program.cs
using System;
using UserManagerApp.BL;
using UserManagerApp.Entities;

namespace UserManagerApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrudService<User> userService = new UserService();
            UserManager userManager = new UserManager(userService);
            ConsoleUI ui = new ConsoleUI(userManager);

            ui.Ejecutar();
        }
    }
}