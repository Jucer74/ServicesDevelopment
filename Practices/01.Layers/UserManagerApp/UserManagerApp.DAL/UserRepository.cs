using System.Collections.Generic;
using UserManagerApp.Entities;
namespace UserManagerApp.DAL;

public class UserRepository
{
    private static List<User> _users = new List<User>();
    private static int nextId = 1;

        public void AddUser(User user)
        {
            if (user.Id == null)
            {
                user.Id = nextId++;
            }
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
}