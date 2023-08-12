using Newtonsoft.Json;

namespace STUDENTS.Models;

// Aquí se define el modelo de datos
public class Students
{
    [JsonProperty("id")]
    public int id { get; set; }
    [JsonProperty("firstName")]
    public string? firstName { get; set; }
    [JsonProperty("lastName")]
    public string? lastName { get; set; }
    [JsonProperty("dateTime")]
    public DateTime dateTime { get; set; }
    [JsonProperty("sex")]
    public string? sex { get; set; }
}


