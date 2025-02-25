// BL/UserService.cs
using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.BL
{
    public class UserService : ICrudService<User>
    {
        private readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public List<User> GetAll()
        {
            return _users;
        }
    }
}