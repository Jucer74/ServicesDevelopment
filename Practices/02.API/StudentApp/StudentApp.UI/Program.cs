using System;
using System.Threading.Tasks;
using StudentApp.Entities;
using StudentApp.BL;

namespace StudentApp.UI
{
class Program
{
    static async Task Main()
    {
        var service = new StudentService();
        while (true)
        {
            Console.WriteLine("1. Ver estudiantes\n2. Agregar estudiante\n3. Actualizar estudiante\n4. Eliminar estudiante\n5. Salir");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine(await service.GetStudents());
                    break;
                case "2":
                    Console.Write("Nombre: ");
                    var firstName = Console.ReadLine();
                    Console.Write("Apellido: ");
                    var lastName = Console.ReadLine();
                    Console.Write("Fecha de nacimiento (YYYY/MM/DD): ");
                    var dateOfBirth = Console.ReadLine();
                    Console.Write("Sexo (M/F): ");
                    var sex = Console.ReadLine();
                    Console.WriteLine(await service.AddStudent(firstName, lastName, dateOfBirth, sex));
                    break;
                case "3":
                    Console.Write("ID del estudiante: ");
                    var id = int.Parse(Console.ReadLine());
                    Console.Write("Nuevo nombre: ");
                    firstName = Console.ReadLine();
                    Console.Write("Nuevo apellido: ");
                    lastName = Console.ReadLine();
                    Console.Write("Nueva fecha de nacimiento (YYYY/MM/DD): ");
                    dateOfBirth = Console.ReadLine();
                    Console.Write("Nuevo sexo (M/F): ");
                    sex = Console.ReadLine();
                    Console.WriteLine(await service.UpdateStudent(id, firstName, lastName, dateOfBirth, sex));
                    break;
                case "4":
                    Console.Write("ID del estudiante: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine(await service.DeleteStudent(id));
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
}