namespace TeamsApi.Dtos;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Coach { get; set; } = null!;
    public string Conference{ get; set; } = null!;
}