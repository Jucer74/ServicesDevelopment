using System;
using System.Threading.Tasks;
using StudentApp.DAL; 
using StudentApp.Entities; 

namespace StudentApp.BL
{
public class StudentService
{
    private readonly StudentRepository _repo = new();

    public async Task<string> GetStudents() => await _repo.GetStudentsAsync();

    public async Task<string> AddStudent(string firstName, string lastName, string dateOfBirth, string sex)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(dateOfBirth) || string.IsNullOrWhiteSpace(sex))
            return "Todos los campos son obligatorios.";

        return await _repo.AddStudentAsync(firstName, lastName, dateOfBirth, sex);
    }

    public async Task<string> UpdateStudent(int id, string firstName, string lastName, string dateOfBirth, string sex)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(dateOfBirth) || string.IsNullOrWhiteSpace(sex))
            return "ID y todos los campos son obligatorios.";

        return await _repo.UpdateStudentAsync(id, firstName, lastName, dateOfBirth, sex);
    }

    public async Task<string> DeleteStudent(int id)
    {
        if (id <= 0) return "ID inválido.";
        return await _repo.DeleteStudentAsync(id);
    }
}
}
