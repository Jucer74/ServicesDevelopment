using UserManagerApp.BL;
using UserManagerApp.UI;

namespace UserManagerApp
{
    class Program
    {
        static void Main()
        {
            UserService userService = new UserService();
            MenuHandler menuHandler = new MenuHandler(userService);
            menuHandler.ShowMenu();
        }
    }
}
