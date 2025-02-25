using UserManagementApp.BL;
using UserManagementApp.DAL;
using System;
using System.Globalization;

/***********/
/*-- Main -*/
/***********/
UserService userService = new UserService();

try
{
    Menu();
}
catch (Exception e)
{
    Console.WriteLine(e);
}

/*****************/
/*-- Functions --*/
/*****************/
void Menu()
{
    var option = ' ';

    while (option != '0')
    {
        Console.Clear();
        Console.WriteLine(" User Management App ");
        Console.WriteLine("------------------------------");
        Console.WriteLine("1. Agregar Usuario");
        Console.WriteLine("2. Listar Usuarios");
        Console.WriteLine("3. Actualizar Usuario");
        Console.WriteLine("4. Eliminar Usuario");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
        option = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (option)
        {
            case '0':
                Console.WriteLine("Saliendo...");
                break;
            case '1':
                InsertUser();
                break;
            case '2':
                GetUsers();
                break;
            case '3':
                UpdateUser();
                break;
            case '4':
                DeleteUser();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}

void GetUsers()
{
    Console.Clear();
    Console.WriteLine("Lista de Usuarios");
    Console.WriteLine("----------------");
    Console.WriteLine();

    var users = userService.GetUsers();

    foreach (var user in users)
    {
        DisplayUser(user);
    }

    Console.WriteLine("\n({0}) Usuarios recuperados", users.Count);
    Console.WriteLine();
}

void DisplayUser(User user)
{
    Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
}

void InsertUser()
{
    Console.Clear();
    Console.WriteLine("Agregar Nuevo Usuario");
    Console.WriteLine("---------------------");
    Console.WriteLine();

    var newUser = CreateUser();

    if (userService.AddUser(newUser))
    {
        Console.WriteLine("\nEl usuario fue agregado exitosamente\n");
    }
    else
    {
        Console.WriteLine("\nEl usuario no pudo ser agregado\n");
    }
}

User CreateUser()
{
    Console.Write("Nombre: ");
    var name = Console.ReadLine();
    Console.Write("Email: ");
    var email = Console.ReadLine();

    var newUser = new User
    {
        Id = userService.GetUsers().Count + 1,
        Name = name!,
        Email = email!
    };

    return newUser;
}

void UpdateUser()
{
    Console.Clear();
    Console.WriteLine("Actualizar Usuario");
    Console.WriteLine("-------------------");
    Console.WriteLine();
    
    Console.Write("Ingrese el ID del usuario a actualizar: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var user = userService.GetUsers().Find(u => u.Id == id);
        if (user != null)
        {
            Console.Write("Nuevo Nombre: ");
            user.Name = Console.ReadLine();
            Console.Write("Nuevo Email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Usuario actualizado correctamente!");
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

void DeleteUser()
{
    Console.Clear();
    Console.WriteLine("Eliminar Usuario");
    Console.WriteLine("----------------");
    Console.WriteLine();
    
    Console.Write("Ingrese el ID del usuario a eliminar: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var user = userService.GetUsers().Find(u => u.Id == id);
        if (user != null)
        {
            userService.GetUsers().Remove(user);
            Console.WriteLine("Usuario eliminado correctamente!");
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
