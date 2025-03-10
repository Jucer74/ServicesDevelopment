using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using APIClientConsole.Models;

namespace APIClientConsole.Services
{
    public static class StudentService
    {
        // Método para obtener TODOS los estudiantes (sin paginación en el servidor)
        // y luego aplicar paginación en el cliente
        public static async Task GetAllStudents(HttpClient client)
        {
            try
            {
                // Hacemos una petición para obtener todos los estudiantes
                HttpResponseMessage response = await client.GetAsync("");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializamos el JSON a una lista de objetos Student
                List<Student> allStudents = JsonSerializer.Deserialize<List<Student>>(responseBody);

                if (allStudents == null || allStudents.Count == 0)
                {
                    Console.WriteLine("No hay estudiantes para mostrar.");
                    return;
                }

                // Ordenamos la lista numéricamente, convirtiendo el id (string) a entero
                var orderedStudents = allStudents.OrderBy(s => int.Parse(s.id)).ToList();

                int pageSize = 100; // cantidad de registros por página
                int totalPages = (int)Math.Ceiling(orderedStudents.Count / (double)pageSize);
                int currentPage = 0;
                bool continuePaging = true;

                while (continuePaging)
                {
                    // Obtenemos la "página" actual
                    var pageStudents = orderedStudents
                                         .Skip(currentPage * pageSize)
                                         .Take(pageSize)
                                         .ToList();

                    if (pageStudents.Count == 0)
                    {
                        Console.WriteLine("No hay más estudiantes para mostrar.");
                        break;
                    }

                    Console.WriteLine($"Mostrando página {currentPage + 1} de {totalPages}:");
                    foreach (var student in pageStudents)
                    {
                        Console.WriteLine($"{student.id}: {student.first_name} {student.last_name}, {student.sex}, {student.date_of_birth}");
                    }

                    // Preguntar si desea ver la siguiente página
                    if (currentPage + 1 < totalPages)
                    {
                        Console.WriteLine("¿Desea ver los siguientes 100 estudiantes? (s/n)");
                        string input = Console.ReadLine().Trim().ToLower();
                        if (input == "s")
                        {
                            currentPage++;
                        }
                        else
                        {
                            continuePaging = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay más páginas.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los estudiantes: " + ex.Message);
            }
        }
    }
}
