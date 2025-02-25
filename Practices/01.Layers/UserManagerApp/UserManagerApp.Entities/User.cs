namespace UserManagerApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public User (){ }
        public void UpdateUser(User user)
        {
            this.Email = user.Email;
            this.Name = user.Name;
        }
    }
}