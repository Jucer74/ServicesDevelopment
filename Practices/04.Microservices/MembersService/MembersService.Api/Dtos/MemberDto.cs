namespace MembersService.Api.Dtos;

public class MemberDto
{
    public int Id { get; set; }
    public string TeamId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
}