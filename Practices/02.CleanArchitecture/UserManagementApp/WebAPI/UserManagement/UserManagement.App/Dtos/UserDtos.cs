namespace UserManagement.App.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string Username{ get; set; } = null!;
    
    public string Password{ get; set; } = null!;
}