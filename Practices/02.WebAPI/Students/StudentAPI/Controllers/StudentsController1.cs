using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string studentsFilePath = "students.json";

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var studentsJson = System.IO.File.ReadAllText(studentsFilePath);
                var students = JArray.Parse(studentsJson);
                var student = students.FirstOrDefault(s => s["id"].Value<int>() == id);

                if (student == null)
                {
                    return NotFound(new { error = "Estudiante no encontrado." });
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "No se pudo leer el archivo de estudiantes." });
            }
        }

        // Otros métodos HTTP (POST, PUT, DELETE) se pueden agregar aquí.
    }
}
