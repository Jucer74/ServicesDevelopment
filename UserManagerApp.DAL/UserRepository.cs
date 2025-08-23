using System.Collections.Generic;
using UserManagerApp.Entities;

namespace UserManagerApp.DAL
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        public void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
