namespace UserManagerApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // agrege esto porque me salia un error de nulos y asi me funciono.
        public string Email { get; set; } = string.Empty;  
    }
}

