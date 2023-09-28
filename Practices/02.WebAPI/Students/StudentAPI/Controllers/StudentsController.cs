using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string studentsFilePath = "students.json";

        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                var studentsJson = System.IO.File.ReadAllText(studentsFilePath);
                var students = JArray.Parse(studentsJson);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "No se pudo leer el archivo de estudiantes." });
            }
        }

       
    }
}
