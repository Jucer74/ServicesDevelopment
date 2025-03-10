using System;
using System.Net.Http;
using System.Threading.Tasks;
using APIClientConsole.Services;

namespace APIClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Se crea el cliente HTTP y se define la dirección base (la URL de la API)
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/students");

            while (true)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Consultar todos los estudiantes");
                Console.WriteLine("2. Buscar estudiante por ID");
                Console.WriteLine("3. Crear nuevo estudiante");
                Console.WriteLine("4. Editar estudiante");
                Console.WriteLine("5. Eliminar estudiante");
                Console.WriteLine("6. Salir");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await StudentService.GetAllStudents(client);
                        Console.WriteLine("Opción 1");
                        break;
                    case "2":
                        Console.WriteLine("Opción 2");
                        //await GetStudentById(client);
                        break;
                    case "3":
                        Console.WriteLine("Opción 3");
                        //await CreateStudent(client);
                        break;
                    case "4":
                        Console.WriteLine("Opción 4");
                        //await EditStudent(client);
                        break;
                    case "5":
                        Console.WriteLine("Opción 5");
                        //await DeleteStudent(client);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}
