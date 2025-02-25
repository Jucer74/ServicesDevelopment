// DAL/UserRepository.cs
using System.Collections.Generic;

namespace UserManagerApp.DAL
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
    }

    // Entity
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}