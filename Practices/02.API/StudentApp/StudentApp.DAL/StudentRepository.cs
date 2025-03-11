using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentApp.Entities;

namespace StudentApp.DAL{
public class StudentRepository
{
    private readonly HttpClient _client = new();

    public async Task<string> GetStudentsAsync()
    {
        return await _client.GetStringAsync("http://localhost:3000/students");
    }

public async Task<string> AddStudentAsync(string firstName, string lastName, string dateOfBirth, string sex)
    {
        var student = new { first_name = firstName, last_name = lastName, date_of_birth = dateOfBirth, sex };
        var content = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("http://localhost:3000/students", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateStudentAsync(int id, string firstName, string lastName, string dateOfBirth, string sex)
    {
        var student = new { first_name = firstName, last_name = lastName, date_of_birth = dateOfBirth, sex };
        var content = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"http://localhost:3000/students/{id}", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteStudentAsync(int id)
    {
        var response = await _client.DeleteAsync($"http://localhost:3000/students/{id}");
        return await response.Content.ReadAsStringAsync();
    }
}
}