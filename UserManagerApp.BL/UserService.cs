using System.Collections.Generic;
using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.BL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
