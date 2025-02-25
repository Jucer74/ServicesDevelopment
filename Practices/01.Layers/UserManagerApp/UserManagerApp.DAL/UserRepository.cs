// DAL/UserRepository.cs
using System.Collections.Generic;
using System.Linq;

namespace UserManagementApp.DAL
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

        public bool UpdateUser(int id, string newName, string newEmail)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = newName;
                user.Email = newEmail;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                return true;
            }
            return false;
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
