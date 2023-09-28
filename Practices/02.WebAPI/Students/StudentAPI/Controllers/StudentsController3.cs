[HttpPut("{id}")]
public IActionResult UpdateStudent(int id, [FromBody] JObject updatedStudent)
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

        students[studentIndex].Merge(updatedStudent, new JsonMergeSettings
        {
            MergeArrayHandling = MergeArrayHandling.Replace
        });

        System.IO.File.WriteAllText(studentsFilePath, students.ToString());

        return Ok(students[studentIndex]);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { error = "No se pudo guardar los cambios." });
    }
}
