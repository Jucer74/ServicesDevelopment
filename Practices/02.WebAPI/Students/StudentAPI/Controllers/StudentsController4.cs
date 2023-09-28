[HttpDelete("{id}")]
public IActionResult DeleteStudent(int id)
{
    try
    {
        var studentsJson = System.IO.File.ReadAllText(studentsFilePath);
        var students = JArray.Parse(studentsJson);

        var studentIndex = students.FindIndex(s => s["id"].Value<int>() == id);

        if (studentIndex == -1)
        {
            return NotFound(new { error = "Estudiante no encontrado." });
        }

        students.RemoveAt(studentIndex);

        System.IO.File.WriteAllText(studentsFilePath, students.ToString());

        return Ok(new { message = "Estudiante eliminado exitosamente." });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { error = "No se pudo guardar los cambios." });
    }
}
