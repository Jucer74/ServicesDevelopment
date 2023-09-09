namespace TeamsApi.Dtos;

public class TeamMemberDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = null!;
    public int TeamId { get; set; }
}