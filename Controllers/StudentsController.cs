using Microsoft.AspNetCore.Mvc;
using STUDENTS.Models;

namespace STUDENTS.Controllers
{

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        // Aquí inyectamos la dependencia de HttpClientFactory
        private readonly HttpClient _httpClient;
        public StudentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3000/");
        }

        // Aquí definimos los métodos de la API
        // GET: api/students
        [HttpGet]
        public async Task<IEnumerable<Students>> GetStudents()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Students>>("students");
            return response ?? Enumerable.Empty<Students>();
        }

        // GET: api/students/id
        [HttpGet("{id}")]
        public async Task<Students> GetStudentById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Students>($"students/{id}");
            return response ?? new Students();
        }

        // POST: api/students
        [HttpPost]
        public async Task<Students> CreateStudent(Students student)
        {
            var response = await _httpClient.PostAsJsonAsync("students", student);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Students?>() ?? new Students();
        }

        // PUT: api/students/id
        [HttpPut("{id}")]
        public async Task<Students> UpdateStudent(int id, Students student)
        {
            var response = await _httpClient.PutAsJsonAsync($"students/{id}", student);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Students?>() ?? new Students();
        }

        // DELETE: api/students/id
        [HttpDelete("{id}")]
        public async Task<bool> DeleteStudent(int id)
        {
            var response = await _httpClient.DeleteAsync($"students/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}