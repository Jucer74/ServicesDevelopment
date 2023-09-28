[HttpPost]
public IActionResult CreateStudent([FromBody] JObject newStudent)
{
    try
    {
        var studentsJson = System.IO.File.ReadAllText(studentsFilePath);
        var students = JArray.Parse(studentsJson);

        newStudent["id"] = students.Count + 1;
        students.Add(newStudent);

        System.IO.File.WriteAllText(studentsFilePath, students.ToString());

        return CreatedAtAction("GetStudentById", new { id = newStudent["id"].Value<int>() }, newStudent);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { error = "No se pudo escribir en el archivo de estudiantes." });
    }
}
