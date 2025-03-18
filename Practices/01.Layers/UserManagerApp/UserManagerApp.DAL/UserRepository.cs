// DAL/UserRepository.cs
using System.Collections.Generic;
using UserManagementApp.Entities;

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
    }
}