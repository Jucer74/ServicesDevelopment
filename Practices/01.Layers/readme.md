# Practica Layers
A continuacion realizaremos un ejercicio practico creando un proyecto donde vamos a integrar las capas (Layers) principales (UI, BL, DAL) para mostrar su uso.

Recuerde realizar el desarrollo de este ejercicio en su pripia rama.

# Practica
Crearemos una aplicación simple de gestión de usuarios, donde podrás agregar y listar usuarios. 

Vamos a dividir el proyecto en tres capas:

- *Capa de Presentación (UI):* Interfaz de usuario.
- *Capa de Lógica de Negocio (BL):* Maneja la lógica de la aplicación.
- *Capa de Acceso a Datos (DAL):* Interactúa con la base de datos.

Ejecute los siguientes pasos en el Directorio de /Practices/01.Layers

## Paso 1: Crear el Proyecto

1. Crea un nuevo proyecto de tipo Aplicación de Consola llamado *UserManagementApp*.
```code
dotnet new console -n UserManagementApp
```
2. Ingrese al directorio *UserManagementApp*. 
```code
cd UserManagementApp
```
3. Abra Visual Studio en el directorio actual
```code
Code .
```

## Paso 2: Estructura del Proyecto
Dentro del proyecto, cree las siguientes carpetas para organizar las capas:

```code
UserManagementApp/
│
├── UI/          # Capa de Presentación
├── BL/          # Capa de Lógica de Negocio
├── DAL/         # Capa de Acceso a Datos
└── Program.cs   # Punto de entrada de la aplicación
```

## Paso 3: Implementar la Capa de Acceso a Datos (DAL)
Crea un archivo *UserRepository.cs* en la carpeta *DAL*:

```csharp
// DAL/UserRepository.cs
using System.Collections.Generic;

namespace UserManagementApp.DAL
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
    }

    // Entity
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
```

## Paso 4: Implementar la Capa de Lógica de Negocio (BL)
Crea un archivo *UserService.cs* en la carpeta *BL*:

```csharp
// BL/UserService.cs
using UserManagementApp.DAL;

namespace UserManagementApp.BL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            // Aquí podrías agregar validaciones o lógica adicional
            _userRepository.AddUser(user);
        }

        public List<User> GetUsers()
        {
            // Aquí podrías agregar lógica adicional, como filtros o transformaciones
            return _userRepository.GetUsers();
        }
    }
}
```

## Paso 5: Implementar la Capa de Presentación (UI)
Modifica el archivo *Program.cs* en la raíz del proyecto para interactuar con el usuario:

```csharp
// Program.cs
using System;
using UserManagementApp.BL;
using UserManagementApp.DAL;

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
                    Console.WriteLine("Usuario agregado con éxito!");
                }
                else if (option == "2")
                {
                    var users = userService.GetUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}");
                    }
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
    }
}
```

## Paso 6: Ejecutar la Aplicación
1. Abre una terminal en VS Code (Ctrl + ).
2. Ejecuta el proyecto:
```code
dotnet run
```
3. La aplicación se ejecutará en la terminal, y podrás interactuar con ella.

## Nota: Configurar Namespaces y Dependencias
Asegúrate de que los namespaces estén correctamente configurados en cada archivo. No es necesario agregar referencias adicionales, ya que todas las clases están en el mismo proyecto.


# Resumen
- *Capa de Presentación (UI):* Program.cs maneja la interacción con el usuario.
- *Capa de Lógica de Negocio (BL):* UserService.cs contiene la lógica de la aplicación.
- *Capa de Acceso a Datos (DAL):* UserRepository.cs maneja el almacenamiento de datos.


# Practica

## Paso 1: Crear la Solución y los Proyectos
1. Crea una nueva version del proyecto, en un nuevo directorio llamado *UserManagerApp*
```code
mkdir UserManagerApp
cd UserManagemerApp
```
2. Cree una nueva solucion con el mismo nombre
```Code
dotnet new sln -n UserManagerApp
```
3. Cree los proyectos correspondinetes a cada Capa
```code
dotnet new console -n UserManagerApp.UI
dotnet new classlib -n UserManagerApp.BL
dotnet new classlib -n UserManagerApp.DAL
dotnet new classlib -n UserManagerApp.Entities
```
4. Agregue los proyectos a la solucion
```code
dotnet sln UserManagerApp.sln add UserManagerApp.UI/UserManagerApp.UI.csproj
dotnet sln UserManagerApp.sln add UserManagerApp.BL/UserManagerApp.BL.csproj
dotnet sln UserManagerApp.sln add UserManagerApp.DAL/UserManagerApp.DAL.csproj
dotnet sln UserManagerApp.sln add UserManagerApp.Entities/UserManagerApp.Entities.csproj
```

## Paso 2: Configurar las Dependencias
- *UserManagerApp.UI* debe referenciar a *UserManagerApp.BL* y *UserManagerApp.Entities*.
- *UserManagerApp.BL* debe referenciar a *UserManagerApp.DAL* y *UserManagerApp.Entities*.
- *UserManagerApp.DAL* debe referenciar a *UserManagerApp.Entities*.

Ejecuta los siguientes comandos para agregar las referencias:
```code
dotnet add UserManagerApp.UI/UserManagerApp.UI.csproj reference UserManagerApp.BL/UserManagerApp.BL.csproj
dotnet add UserManagerApp.UI/UserManagerApp.UI.csproj reference UserManagerApp.Entities/UserManagerApp.Entities.csproj
dotnet add UserManagerApp.BL/UserManagerApp.BL.csproj reference UserManagerApp.DAL/UserManagerApp.DAL.csproj
dotnet add UserManagerApp.BL/UserManagerApp.BL.csproj reference UserManagerApp.Entities/UserManagerApp.Entities.csproj
dotnet add UserManagerApp.DAL/UserManagerApp.DAL.csproj reference UserManagerApp.Entities/UserManagerApp.Entities.csproj 
```

## Paso 3: Codifique
Cree el codigo fuente correspondiente a cada capa segun corresponda asi:

- *UI:* Program.cs
- *BL:* UserService.cs
- *DAL:* UserRepository.cs
- *Entities:* User.cs

Al Finalizar debe tener como resultado la siguiente estructura de la solucion
```Code
UserManagerApp/
│
├── UserManagerApp.sln
│
├── UserManagerApp.UI/
│   ├── Program.cs
│   └── UserManagerApp.UI.csproj
│
├── UserManagerApp.BL/
│   ├── UserService.cs
│   └── UserManagerApp.BL.csproj
│
├── UserManagerApp.DAL/
│   ├── UserRepository.cs
│   └── UserManagerApp.DAL.csproj
│
└── UserManagerApp.Entities/
    ├── User.cs
    └── UserManagerApp.Entities.csproj
```

## Paso 4: Ejecutar la Aplicación
Establece *UserManagerApp.UI* como proyecto de inicio:
```code
dotnet build
dotnet run --project UserManagerApp.UI/UserManagerApp.UI.csproj
```
La aplicación se ejecutará en la terminal, y podrás interactuar con ella.

## Paso 5: Buenas Practicas
Mejore el codigo de la capa de presentacion, para que se realice el llamado a cada Opcion utilizando una funcion y Valide que el valor digitado sea solo un numero y no letras.

### Ayuda
Mire el repository de SOLID como base de este punto.


## Nota: Namespaces y Dependencias
Asegúrate de que los namespaces estén correctamente configurados en cada archivo y que los *using* de las capas dependientes esten correctas.

- *UI:* BL, Entities
- *BL:* DAL, Entites
- *DAL:* Entities

